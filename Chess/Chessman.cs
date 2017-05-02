using System.Windows.Controls;

namespace Chess
{
    abstract class Chessman
    {
        private bool isWhite;
        protected bool IsWhite
        {
            get
            {
                return isWhite;
            }

            set
            {
                isWhite = value;
            }
        }

        //public bool IsWhite { get => IsWhite1; set => IsWhite1 = value; }

        protected string color;
        public string Color
        {
            get
            {
                return color;
            }
        }
        //public string Color { get => color; }

        private StackPanel view;
        public StackPanel View
        {
            get
            {
                return view;
            }

            set
            {
                view = value;
            }
        }
        //public StackPanel View { get => view; set => view = value; }

        private string current_position;
        public string Current_position
        {
            get
            {
                return current_position;
            }

            set
            {
                current_position = value;
            }
        }
        //public string Current_Position { get => current_position; set => current_position = value; }

        protected string desc;
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public abstract void Move(MyButton source, MyButton dest);

    }
}
