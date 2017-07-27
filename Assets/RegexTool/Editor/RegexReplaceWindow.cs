namespace RegexTool
{
    using UnityEngine;
    using UnityEditor;
    using System.Text.RegularExpressions;
    using System.Linq;

    public class RegexReplaceWindow : EditorWindow
    {
        const float ButtonWidth = 38f;
        [SerializeField] private string baseText = "";
        [SerializeField] private string resultText = "";
        [SerializeField] private string pattern = "";
        [SerializeField] private string replacement = "";

        private GUIStyle grayishTextAreaStyle;

        [MenuItem("Tools/Regex Tool/Regex Replace")]
        static void Open()
        {
            CreateInstance<RegexReplaceWindow>().Show();
        }

        static GUIStyle CreateGrayishStyle()
        {
            var style = new GUIStyle(EditorStyles.textArea);
            style.normal.textColor = Color.gray;
            style.hover.textColor = Color.gray;
            style.focused.textColor = Color.gray;
            style.focused.background = style.normal.background;
            return style;
        }

        void OnGUI()
        {
            if (this.grayishTextAreaStyle == null)
            {
                this.grayishTextAreaStyle = CreateGrayishStyle();
            }

            EditorGUILayout.LabelField("Regexパターンに従って文字列を置換します");

            var centerLabelWidth = 30f;
            var height = GUILayout.Height(Screen.height - 166f);
            var width = GUILayout.Width(Screen.width / 2f - centerLabelWidth / 2f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(width);
            EditorGUILayout.LabelField("置換前", width);
            this.baseText = EditorGUILayout.TextArea(this.baseText, width, height);
            EditorGUILayout.EndVertical();

            var style = new GUIStyle(GUI.skin.GetStyle("Label"));
            style.alignment = TextAnchor.MiddleCenter;

            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            EditorGUILayout.LabelField("", centeredStyle, GUILayout.Width(centerLabelWidth - 18f));

            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25 - 33, 100, 50), "->", centeredStyle);

            EditorGUILayout.BeginVertical(width);
            EditorGUILayout.LabelField("置換後");

            EditorGUILayout.SelectableLabel(this.resultText, this.grayishTextAreaStyle, width, height);

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Pattern");
            this.pattern = EditorGUILayout.TextField(this.pattern);

            EditorGUILayout.LabelField("Replacement");
            this.replacement = EditorGUILayout.TextField(this.replacement);

            bool emptyBaseText = string.IsNullOrEmpty(this.baseText);
            bool emptyPattern = string.IsNullOrEmpty(this.pattern);
            EditorGUI.BeginDisabledGroup(emptyBaseText | emptyPattern);
            GUILayout.Space(6f);
            
            if (GUILayout.Button("置換"))
            {
                this.resultText = this.baseText.Split('\n')
                .Select(text => Regex.Replace(text, this.pattern, this.replacement))
                .Aggregate((s, next) => s + "\n" + next);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
