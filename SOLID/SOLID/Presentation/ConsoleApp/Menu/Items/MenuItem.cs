namespace SOLID.Presentation.ConsoleApp.Menu.Items
{
    abstract class MenuItem
    {
        public string Text { get; }

        public MenuItem(string text)
        {
            Text = text;
        }

        public abstract void OnLeft();
        public abstract void OnRight();
        public abstract void OnSelect();

        public virtual string GetDisplayText()
        {
            return Text;
        }
    }
}
