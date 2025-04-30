using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Menu.Items
{
    class SettingMenuItem : MenuItem
    {
        private readonly ICommand _onLeft;
        private readonly ICommand _onRight;
        private readonly Func<string> _getValueFunc;

        public SettingMenuItem(string text, Func<string> getValue, ICommand onLeft, ICommand onRight)
            : base(text)
        {
            _getValueFunc = getValue;
            _onLeft = onLeft;
            _onRight = onRight;
        }

        public override void OnLeft() => _onLeft?.Execute();
        public override void OnRight() => _onRight?.Execute();
        public override void OnSelect() { }

        public override string GetDisplayText()
        {
            return $"{Text}: {_getValueFunc.Invoke()}";
        }
    }
}
