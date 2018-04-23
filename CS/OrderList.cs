using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace CustomDateFilterPopup {
    class Order {
        const int factor = 1000;
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Customer { get; set; }

        public Order(int index) {
            OrderNumber = factor + index;
            OrderDate = DateTime.Today.AddDays(-index);
            Customer = "Customer " + index;
        }
    }

    class OrderList : BindingList<Order> {
        Random rnd;
        const int max = 7;
        public OrderList(int count) {
            rnd = new Random();
            for(int i = 0; i < count; i++)
                Items.Add(new Order(rnd.Next(max)));
        }
    }
}
