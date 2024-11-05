using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Observable_Immutable_Concurrent
{
    /// <summary>
    /// Реализует магазин
    /// </summary>
    internal class Shop
    {
        private readonly ObservableCollection<Item> _productList = new();
        private int _сount = 0;

        /// <summary>
        /// Добавляет новый товар
        /// </summary>
        /// <param name="productName">Название продукта</param>
        public void Add(string productName)
        {
            _productList.Add(
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
            var itemToRemove = _productList.FirstOrDefault(item => item.Id == id);
            if ( itemToRemove != null )
            {
                _productList.Remove(itemToRemove);
            }
        }

        /// <summary>
        /// Подписка на изменения в товарном ассортименте
        /// </summary>
        /// <param name="handler">Обработчик события</param>
        public void SubscribeToChangeProductCollection(NotifyCollectionChangedEventHandler handler)
        {
            _productList.CollectionChanged += handler;
        }

        private int GetId()
        {
            return _сount++;
        }
    }
}
