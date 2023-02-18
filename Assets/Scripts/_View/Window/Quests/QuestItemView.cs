
using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class QuestItemView : WindowItem
    {
        [SerializeField] protected WindowType Type;
        private SignalBus _signalBus;

        private Image _image;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _image = GetComponent<Image>();

            WindowType = Type;
        }
    }
}
