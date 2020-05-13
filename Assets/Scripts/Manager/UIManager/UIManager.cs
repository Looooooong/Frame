using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UICtrl : MonoBehaviour {
    public Dictionary<string, GameObject> view = new Dictionary<string, GameObject>();

    private void load_all_object(GameObject root, string path) {
        foreach (Transform tf in root.transform) {
            if (this.view.ContainsKey(path + tf.gameObject.name)) {
                // Debugger.LogWarning("Warning object is exist:" + path + tf.gameObject.name + "!");
                continue;
            }
            this.view.Add(path + tf.gameObject.name, tf.gameObject);
            load_all_object(tf.gameObject, path + tf.gameObject.name + "/");
        }

    }

    public virtual void Awake() {
        this.load_all_object(this.gameObject, "");
    }


    public void add_button_listener(string view_name, UnityAction onclick) {
        Button bt = this.view[view_name].GetComponent<Button>();
        if (bt == null)
        {
            Debugger.LogWarning("UI_manager add_button_listener: not Button Component!");
            return;
        }

        bt.onClick.AddListener(onclick);
    }

    public void add_slider_listener(string view_name, UnityAction<float> on_value_changle) {
        Slider s = this.view[view_name].GetComponent<Slider>();
        if (s == null)
        {
            Debugger.LogWarning("UI_manager add_slider_listener: not Slider Component!");
            return;
        }

        s.onValueChanged.AddListener(on_value_changle);
    }
}

public class UIManager : UnitySingleton<UIManager> {
    public GameObject canvas;

    public override void Awake()
    {
        base.Awake();
        this.canvas = GameObject.Find("Canvas");
        if (this.canvas == null) {
            Debugger.LogError("UI manager load  Canvas failed!!!!!");
        }
    }

    public UICtrl ShowUIView(string name) {
        string path = "GUI/UI_Prefabs/" + name + ".prefab";
        //GameObject ui_prefab = (GameObject)ResMgr.Instance.GetAssetCache<GameObject>(path);
        GameObject ui_prefab = Resources.Load<GameObject>(path);
        GameObject ui_view = GameObject.Instantiate(ui_prefab);
        ui_view.name = name;
        ui_view.transform.SetParent(this.canvas.transform, false);

        Type type = Type.GetType(name + "_UICtrl");
        UICtrl ctrl = (UICtrl)ui_view.AddComponent(type);

        return ctrl;
    }

    public void RemoveUIView(string name) {
        Transform view = this.canvas.transform.Find(name);
        if (view) {
            GameObject.Destroy(view.gameObject);
        }
    }

    public void RemoveAllViews() {
        List<Transform> children = new List<Transform>();
        foreach (Transform t in this.canvas.transform) {
            children.Add(t);
        }

        for (int i = 0; i < children.Count; i++) {
            GameObject.Destroy(children[i].gameObject);

        }
    }

}
