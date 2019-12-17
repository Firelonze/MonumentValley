using UnityEngine;
using UnityEditor;
using FlatLighting;
using System.Collections;

public class FlatLightingUnityShadowShaderEditor : FlatLightingShaderEditor {

	private MaterialProperty unityShadowPower = null;

	protected override void FindProperties(MaterialProperty[] properties) {
		base.FindProperties(properties);
        unityShadowPower = FindProperty("_UnityShadowPower", properties);
	}

	protected override void ShaderPropertiesGUI()
    {
        ShowUnityShadowSettings();
        base.ShaderPropertiesGUI();
    }

    private void ShowUnityShadowSettings() {
		using (new UITools.GUIVertical(UITools.VGroupStyle)) {
			UITools.Header(Labels.UnityShadow);
			base.materialEditor.ShaderProperty(unityShadowPower, unityShadowPower.displayName);
			UITools.DrawSeparatorThinLine();
		}
	}
}
