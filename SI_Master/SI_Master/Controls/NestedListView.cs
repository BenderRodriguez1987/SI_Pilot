using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SI_Master.Controls
{
    public class NestedListView: StackLayout
    {
        public static readonly BindableProperty ChildrenListProperty =
            BindableProperty.Create("ChildrenList", typeof(IList<ContentView>), typeof(NestedListView), new List<ContentView>(), propertyChanged: UpdateList);

        public IList<ContentView> ChildrenList
        {
            get { return (IList<ContentView>)GetValue(ChildrenListProperty); }
            set { SetValue(ChildrenListProperty, value); }
        }

        private static void UpdateList(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is IList<ContentView> cells && bindable is NestedListView nestedListView)
            {
                nestedListView.Children.Clear();
                foreach (var cell in cells)
                    nestedListView.Children.Add(cell);
            }
        }
    }
}
