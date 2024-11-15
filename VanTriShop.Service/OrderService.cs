using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Data.Repositories;
using VanTriShop.Model.Models;

namespace VanTriShop.Service
{
	public interface IOrderService
	{
		Order Create(ref Order order, List<OrderDetail> orderDetails);
		void UpdateStatus(int orderId);
		void Save();
	}
	public class OrderService : IOrderService
	{
		IOrderRepository _orderRepository;
		IOrderDetailRepository _orderDetailRepository;
		IUnitOfWork _unitOfWork;

		public OrderService(IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork, IOrderRepository orderRepository)
		{
			_orderDetailRepository = orderDetailRepository;
			_unitOfWork = unitOfWork;
			_orderRepository = orderRepository;
		}
public Order Create(ref Order order, List<OrderDetail> orderDetails)
		{
			try
			{
				_orderRepository.Add(order);
				_unitOfWork.Commit();

				foreach (var orderDetail in orderDetails)
				{
					orderDetail.OrderId = order.Id;
					_orderDetailRepository.Add(orderDetail);
				}
				return order;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public void Save()
		{
			_unitOfWork.Commit();
		}

		public void UpdateStatus(int orderId)
		{
			var order = _orderRepository.GetSingleById(orderId);
			order.Status = true;
			_orderRepository.Update(order);
		}
	}
}
