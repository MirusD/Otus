using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;

namespace MainCollection
{
    /// <summary>
    /// Создает словарь ключ, значение
    /// </summary>
    internal class OtusDictionary
    {
        private const int DefaultCapacity = 32;

        private int count;
        private int capacity;
        private int[] keys;
        private string[] store;

        public OtusDictionary() 
        {
            capacity = DefaultCapacity;
            keys = new int[DefaultCapacity];
            store = new string[DefaultCapacity];
            count = 0;
        }

        private int GetHashIndex(int key)
        {
            return key % capacity;
        }
        private void Resize()
        {
            capacity = capacity * 2;
            int[] newKeys = new int[capacity];
            string[] newStore = new string[capacity];
            
            for (int i = 0; i < store.Length - 1; i++)
            {
                if (store[i] != null)
                {
                    int hashIndex = GetHashIndex(keys[i]);
                    newStore[hashIndex] = store[i];
                    newKeys[hashIndex] = keys[i];
                }
            }

            keys = newKeys;
            store = newStore;
        }

        /// <summary>
        /// Добавить значение в словарь по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <exception cref="ArgumentNullException">Возникает если мы передаем null в качестве значения</exception>
        /// <exception cref="InvalidOperationException">Возникает если при добавлении нового элемента все слоты заняты</exception>
        public void Add(int key, string? value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Значение не должно быть null");
            }

            if (count > capacity)
            {
                Resize();
            }

            int hashIndex = GetHashIndex(key);

            while (store[hashIndex] != null && keys[hashIndex] != key)
            {
                Resize();
                hashIndex = GetHashIndex(key);
            }

            keys[hashIndex] = key;
            store[hashIndex] = value!;
            count++;
        }

        /// <summary>
        /// Получить значение из словаря по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Возвращает значение по ключу с типом string, если не найдено или вне диапазона то null</returns>
        public string? Get(int key)
        {
            int hashIndex = GetHashIndex(key);

            if (hashIndex > capacity - 1)
            {
                return null;
            }

            if (store[hashIndex] != null && keys[hashIndex] == key)
            {
                return store[hashIndex];
            }

            return null;
        }

        /// <summary>
        /// Очистка словаря
        /// </summary>
        public void Clear()
        {
            capacity = DefaultCapacity;
            store = new string[DefaultCapacity];
            keys = new int[DefaultCapacity];
            count = 0;
        }

        public string? this[int key]
        {
            get 
            {
                return Get(key);
            }
            set 
            {
                Add(key, value);
            }
        }
    }
}
