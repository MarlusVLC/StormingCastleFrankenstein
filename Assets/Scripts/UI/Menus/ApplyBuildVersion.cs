using TMPro;
using UnityEngine;

namespace UI.Menus
{
    public class ApplyBuildVersion : MonoBehaviour
    {
        private TextMeshProUGUI _textToBeApplied;

        private void OnValidate()
        {
            if (_textToBeApplied == null)
            {
                _textToBeApplied = GetComponent<TextMeshProUGUI>();
            }
            _textToBeApplied.text = "build v" + Application.version;
        }
    }
}