namespace SOLID.Presentation.ConsoleApp.Menu.Items
{
    class SubMenuItem : MenuItem
    {
        private readonly Func<Menu> _buildSubMenu;

        public SubMenuItem(string text, Func<Menu> buildsubMenu)
            : base(text)
        {
            _buildSubMenu = buildsubMenu;
        }

        public override void OnLeft() { }
        public override void OnRight() { }
        public override void OnSelect()
        {
            Menu bm = _buildSubMenu();
            bm.Show();
        }
    }
}
