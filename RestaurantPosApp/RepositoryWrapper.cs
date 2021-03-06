﻿using RestaurantPosApp.Contracts;
using RestaurantPosApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
        private IIngredientRepository _ingredient;
        private IInventoryItemRepository _inventoryItem;
        private IMenuCategoryRepository _menuCategory;
        private IMenuItemIngredientRepository _menuItemIngredient;
        private IMenuItemRepository _menuItem;
        private IOrderMenuItemRepository _orderMenuItem;
        private IPlacedOrderRepository _placedOrder;
        private IRestaurantRepository _restaurant;
        private IShoppingListRepository _shoppingList;
        private IShoppingListIngredientRepository _shoppingListIngredient;
        private IEmployeeRepository _employee;
        private IOwnerRepository _owner;

        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IIngredientRepository Ingredient
        {
            get
            {
                if(_ingredient is null)
                {
                    _ingredient = new IngredientRepository(_context);
                }
                return _ingredient;
            }
        }
        public IInventoryItemRepository InventoryItem
        {
            get
            {
                if(_inventoryItem is null)
                {
                    _inventoryItem = new InventoryItemRepository(_context);
                }
                return _inventoryItem;
            }
        }
        public IMenuCategoryRepository MenuCategory
        {
            get
            {
                if(_menuCategory is null)
                {
                    _menuCategory = new MenuCategoryRepository(_context);
                }
                return _menuCategory;
            }
        }
        public IMenuItemIngredientRepository MenuItemIngredient
        {
            get
            {
                if(_menuItemIngredient is null)
                {
                    _menuItemIngredient = new MenuItemIngredientRepository(_context);
                }
                return _menuItemIngredient;
            }
        }
        public IMenuItemRepository MenuItem
        {
            get
            {
                if(_menuItem is null)
                {
                    _menuItem = new MenuItemRepository(_context);
                }
                return _menuItem;
            }
        }
        public IOrderMenuItemRepository OrderMenuItem
        {
            get
            {
                if(_orderMenuItem is null)
                {
                    _orderMenuItem = new OrderMenuItemRepository(_context);
                }
                return _orderMenuItem;
            }
        }
        public IPlacedOrderRepository PlacedOrder
        {
            get
            {
                if(_placedOrder is null)
                {
                    _placedOrder = new PlacedOrderRepository(_context);
                }
                return _placedOrder;
            }
        }
        public IRestaurantRepository Restaurant
        {
            get
            {
                if(_restaurant is null)
                {
                    _restaurant = new RestaurantRepository(_context);
                }
                return _restaurant;
            }
        }
        public IShoppingListRepository ShoppingList
        {
            get
            {
                if(_shoppingList is null)
                {
                    _shoppingList = new ShoppingListRepository(_context);
                }
                return _shoppingList;
            }
        }
        public IShoppingListIngredientRepository ShoppingListIngredient
        {
            get
            {
                if(_shoppingListIngredient is null)
                {
                    _shoppingListIngredient = new ShoppingListIngredientRepository(_context);
                }
                return _shoppingListIngredient;
            }
        }
        public IOwnerRepository Owner
        {
            get
            {
                if(_owner is null)
                {
                    _owner = new OwnerRepository(_context);
                }
                return _owner;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if(_employee is null)
                {
                    _employee = new EmployeeRepository(_context);
                }
                return _employee;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
