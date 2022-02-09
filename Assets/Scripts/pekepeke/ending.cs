using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class Ending : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Text scenarioMessage;
    List<Scenario> scenarios = new List<Scenario>();

    Scenario currentScenario;
    int index = 0;

    class Option
    {
        public string Text;
        public Action Action;
        public Func<bool> IsFlagOK = () => { return true; };
    }
    class Scenario
    {
        public string ScenarioID;
        public List<string> Texts;
        public List<string> Options;
        public string NextScenarioID;
    }
    // Start is called before the first frame update
    void Start()
    {
        var scenario01 = new Scenario()
        {
            ScenarioID = "scenario01",
            Texts = new List<string>()
            {
                "�e�X�g���͂P",
                "�e�X�g���͂Q",
                "�e�X�g���͂R",
                "�e�X�g���͂S",
                "�e�X�g���͂T"
            }

        };

        SetScenario(scenario01);



    }


    // Update is called once per frame
    void Update()
    {
        if (currentScenario != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetNextMessage();
            }
        }
        else if (currentScenario == null)
        {
            Debug.Log("�^�C�g����");
        }
    }

    void SetScenario(Scenario scenario)
    {
        currentScenario = scenario;
        scenarioMessage.text = currentScenario.Texts[0];
    }

    void SetNextMessage()
    {
        if (currentScenario.Texts.Count > index + 1)
        {
            index++;
            scenarioMessage.text = currentScenario.Texts[index];
        }
        else
        {
            ExitScenario();
        }
    }

    void ExitScenario()
    {
        scenarioMessage.text = "";
        index = 0;
        if (string.IsNullOrEmpty(currentScenario.NextScenarioID))
        {
            currentScenario = null;
        }
        else
        {
            var nextScenario = scenarios.Find
                (s => s.ScenarioID == currentScenario.NextScenarioID);
            currentScenario = nextScenario;
        }
    }

}
