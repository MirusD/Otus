using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Observable_Immutable_Concurrent
{
    /// <summary>
    /// Реализует магазин
    /// </summary>
    internal class Shop
    {
        private ObservableCollection<Item> ProductList = null;
        private int Count = 0;
        public Shop() 
        {
            ProductList = new ObservableCollection<Item>();
        }

        /// <summary>
        /// Добавляет новый товар
        /// </summary>
        /// <param name="productName">Название продукта</param>
        public void Add(string productName)
        {
            ProductList.Add(
                new Item()
                {
                    Id = GetId(),
                    Name = productName
                }
            ); ;
        }

        /// <summary>
        /// Удаляет товар
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        public void Remove(int id)
        {
            var itemToRemove = ProductList.FirstOrDefault(item => item.Id == id);
            if ( itemToRemove != null )
            {
                ProductList.Remove(itemToRemove);
            }
        }

        /// <summary>
        /// Подписка на изменения в товарном ассортименте
        /// </summary>
        /// <param name="handler">Обработчик события</param>
        public void SubscribeToChangeProductCollection(NotifyCollectionChangedEventHandler handler)
        { 
            if (ProductList != null) 
            {
                ProductList.CollectionChanged += handler;
            }
        }

        private int GetId()
        {
            return Count++;
        }
    }
}
