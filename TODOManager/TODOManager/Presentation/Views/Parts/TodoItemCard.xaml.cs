using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TODOManager.Presentation.ViewModels.Contents;

namespace TODOManager.Presentation.Views
{
    /// <summary>
    /// TodoItemCard.xaml の相互作用ロジック
    /// </summary>
    public partial class TodoItemCard : UserControl
    {
        public TodoItemCard()
        {
            InitializeComponent();
        }

        //依存関係プロパティ
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register(
                nameof(Item), //プロパティ名
                typeof(TodoItemVM), //バインドするデータの型
                typeof(TodoItemCard), //自分自身の型
                new PropertyMetadata(null) //初期値
        );
        //子要素のカードに渡すコマンド
        public static readonly DependencyProperty ChildDoneCommandProperty =
            DependencyProperty.Register(
                nameof(ChildDoneCommand), //プロパティ名
                typeof(ICommand), //バインドするデータの型
                typeof(TodoItemCard), //自分自身の型
                new PropertyMetadata(null) //初期値
        );

        public TodoItemVM Item
        {
            get => (TodoItemVM)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }
        public ICommand ChildDoneCommand
        {
            get => (ICommand)GetValue(ChildDoneCommandProperty);
            set => SetValue(ChildDoneCommandProperty, value);
        }
    }
}
