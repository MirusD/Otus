using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Menu.Items
{
    class ActionMenuItem : MenuItem
    {
        private readonly ICommand _onSelect;

        public ActionMenuItem(string text, ICommand onSelect) : base(text)
        {
            _onSelect = onSelect;
        }

        public override void OnLeft() {}
        public override void OnRight() {}
        public override void OnSelect() => _onSelect?.Execute();
    }
}
