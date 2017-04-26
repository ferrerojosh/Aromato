﻿using System;
using System.Collections.Generic;
using System.Linq;
using Aromato.Domain.Aggregate;
using Aromato.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure.Repository
{
    public class EfInventoryRepository : IInventoryRepository
    {
        private readonly EfUnitOfWork _unitOfWork;

        public EfInventoryRepository(EfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Inventory FindById(Guid id)
        {
            return _unitOfWork.Inventories.Find(id);
        }

        public IEnumerable<Inventory> FindAll()
        {
            return _unitOfWork.Inventories.ToList();
        }

        public void Add(Inventory entity)
        {
            _unitOfWork.Inventories.Add(entity);
        }

        public void Modify(Inventory entity)
        {
            _unitOfWork.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Inventory entity)
        {
            _unitOfWork.Inventories.Remove(entity);
        }

        public Inventory FindByName(string name)
        {
            return _unitOfWork.Inventories.Include(i => i.Items).First(inventory => inventory.Name == name);
        }
    }
}