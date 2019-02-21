using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RedNeuronalCharacter : MonoBehaviour
{
    private RedNeuronal red;
    public int numInput = 3, numHidden = 6, numOutput = 6;
    
    //INPUTS 
    //  0 =distancia   ;   1=estadoEspada  ;   2=pegando
    // OUTPUT:
    // Attack ; MoveTo ; MoveAway ; Stay ; SwordUp ; SwordDown ; Jump

private static float[,] entrenamiento =
            {
                //Fomenta el avance
                { 6.0f, 1.0f, 0.0f,             0.1f, 0.9f, 0.1f, 0.4f, 0.2f, 0.2f, 0.1f },
                { 4.0f, 1.0f, 0.0f,             0.1f, 0.9f, 0.1f, 0.4f, 0.2f, 0.2f, 0.1f },
                { 3.0f, 0.0f, 0.0f,             0.6f, 0.6f, 0.4f, 0.2f, 0.4f, 0.2f, 0.1f },
                { 3.0f, 1.0f, 0.0f,             0.6f, 0.6f, 0.2f, 0.2f, 0.4f, 0.4f, 0.1f },
                { 2.5f, 2.0f, 0.0f,             0.6f, 0.7f, 0.3f, 0.2f, 0.3f, 0.4f, 0.1f },
                { 4.0f, 0.0f, 0.0f,             0.7f, 0.7f, 0.1f, 0.1f, 0.8f, 0.4f, 0.1f },
                { 4.0f, 2.0f, 0.0f,             0.7f, 0.7f, 0.1f, 0.1f, 0.1f, 0.9f, 0.1f },
                { 9.0f, 1.0f, 0.0f,             0.1f, 0.9f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f },
                { 3.0f, 0.0f, 0.0f,             0.5f, 0.7f, 0.1f, 0.1f, 0.4f, 0.7f, 0.1f },
                { 3.0f, 1.0f, 0.0f,             0.5f, 0.1f, 0.1f, 0.1f, 0.5f, 0.5f, 0.1f },
                { 3.0f, 2.0f, 0.0f,             0.5f, 0.7f, 0.1f, 0.1f, 0.7f, 0.1f, 0.1f },
                { 2.5f, 0.0f, 0.0f,             0.8f, 0.8f, 0.5f, 0.1f, 0.2f, 0.2f, 0.1f },
                { 2.5f, 1.0f, 0.0f,             0.8f, 0.8f, 0.5f, 0.1f, 0.2f, 0.2f, 0.1f },
                { 2.5f, 2.0f, 0.0f,             0.8f, 0.8f, 0.5f, 0.1f, 0.2f, 0.2f, 0.1f },
                { 3.0f, 0.0f, 1.0f,             0.1f, 0.7f, 0.1f, 0.1f, 0.7f, 0.1f, 0.1f },
                { 3.0f, 2.0f, 1.0f,             0.1f, 0.7f, 0.1f, 0.1f, 0.1f, 0.7f, 0.1f },
                { 3.5f, 1.0f, 1.0f,             0.1f, 0.7f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f },
                { 3.5f, 0.0f, 1.0f,             0.7f, 0.7f, 0.2f, 0.1f, 0.7f, 0.1f, 0.1f },
                { 3.5f, 2.0f, 1.0f,             0.7f, 0.5f, 0.5f, 0.1f, 0.1f, 0.7f, 0.1f },

                //Fomenta el ataque
                /*
                { 6.0f, 1.0f, 0.0f,             0.7f, 0.9f, 0.1f, 0.4f, 0.2f, 0.2f, 0.1f },
                { 4.0f, 1.0f, 0.0f,             0.7f, 0.9f, 0.1f, 0.4f, 0.2f, 0.2f, 0.1f },
                { 3.0f, 0.0f, 0.0f,             0.7f, 0.6f, 0.4f, 0.2f, 0.4f, 0.2f, 0.1f },
                { 3.0f, 1.0f, 0.0f,             0.7f, 0.6f, 0.2f, 0.2f, 0.4f, 0.4f, 0.1f },
                { 2.5f, 2.0f, 0.0f,             0.7f, 0.7f, 0.3f, 0.2f, 0.3f, 0.4f, 0.1f },
                { 4.0f, 0.0f, 0.0f,             0.7f, 0.7f, 0.1f, 0.1f, 0.8f, 0.4f, 0.1f },
                { 4.0f, 2.0f, 0.0f,             0.7f, 0.7f, 0.1f, 0.1f, 0.1f, 0.9f, 0.1f },
                { 9.0f, 1.0f, 0.0f,             0.7f, 0.9f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f },
                { 3.0f, 0.0f, 0.0f,             0.5f, 0.7f, 0.1f, 0.1f, 0.4f, 0.7f, 0.1f },
                { 3.0f, 1.0f, 0.0f,             0.5f, 0.1f, 0.1f, 0.1f, 0.5f, 0.5f, 0.1f },
                { 3.0f, 2.0f, 0.0f,             0.5f, 0.7f, 0.1f, 0.1f, 0.7f, 0.1f, 0.1f },
                { 2.5f, 0.0f, 0.0f,             0.8f, 0.8f, 0.5f, 0.1f, 0.2f, 0.2f, 0.1f },
                { 2.5f, 1.0f, 0.0f,             0.8f, 0.8f, 0.5f, 0.1f, 0.2f, 0.2f, 0.1f },
                { 2.5f, 2.0f, 0.0f,             0.8f, 0.8f, 0.5f, 0.1f, 0.2f, 0.2f, 0.1f },
                { 3.0f, 0.0f, 1.0f,             0.6f, 0.7f, 0.1f, 0.1f, 0.7f, 0.1f, 0.1f },
                { 3.0f, 2.0f, 1.0f,             0.6f, 0.7f, 0.1f, 0.1f, 0.1f, 0.7f, 0.1f },
                { 3.5f, 1.0f, 1.0f,             0.6f, 0.7f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f },
                { 3.5f, 0.0f, 1.0f,             0.7f, 0.7f, 0.2f, 0.1f, 0.7f, 0.1f, 0.1f },
                { 3.5f, 2.0f, 1.0f,             0.7f, 0.5f, 0.5f, 0.1f, 0.1f, 0.7f, 0.1f },
                */
        };

    public static RedNeuronalCharacter instance;

    //Inicializacion
    void Start()
    {
        instance = this;
        red = new RedNeuronal(numInput, numHidden, numOutput);
       
        EntrenarRed();
    }

    //Se entrena la red neural con los valores del entrenamiento predeterminado
    private void EntrenarRed()
    {
        float error = 1;
        int epoch = 0;

        //Se reentrena numerosas veces ya que los primeros pesos son aleatorios y tienes que asegurar un primer entrenamiento optimo
        while ((error > 0.05f) && (epoch < 50000))
        {
            error = 0;
            epoch++;
            for (int i = 0; i < entrenamiento.GetLength(0); i++)
            {
                //Entrenar la red
                for (int j = 0; j < numInput; j++)
                {
                    red.SetInput(j, entrenamiento[i, j]);
                }

                //Ajusta los outputs deseados
                for (int j = numInput; j < numInput + numOutput; j++)
                    red.SetOutputDeseado(j - numInput, entrenamiento[i, j]);

                //Calculo de los valores de las neuronas
                red.FeedForward();
                error += red.CalcularError();
                red.BackPropagation();
            }
            error /= entrenamiento.GetLength(0);
            // VER COMO EVOLUCIONA EL ERROR A MEDIDA QUE AVANZAN LOS EPOCHS
        }
    }

    //Vuelve a entrenar la neurona, con los nuevos valores recibidos (asi se fomenta el aprendizaje)
    public void ReentrenarRed(float[] inputs, float[] output)
    {
        float error = 1;
        int epoch = 0;

        while ((error > 0.1f) && (epoch < 1000))
        {
            epoch++;
            //Entrenar la red
            for (int j = 0; j < numInput; j++)
                red.SetInput(j, inputs[j]);

            //Ajusta los outputs deseados
            for (int j = 0; j < numOutput; j++)
                red.SetOutputDeseado(j, output[j]);

            //Calculo de los valores de las neuronas
            red.FeedForward();
            error = red.CalcularError();
            red.BackPropagation();
            // VER COMO EVOLUCIONA EL ERROR A MEDIDA QUE AVANZAN LOS EPOCHS
        }
    }

    //Con los inputs recibidos, te devuelve el output de la accion a realizar
    public byte ConsultarAccion(float[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            red.SetInput(i, inputs[i]);
        }
        red.FeedForward();
        return (byte)red.GetMaxOutputId();
    }

    //Getter de los outputs
    public float GetOutput(int i)
    {
        return red.GetOutput(i);
    }

    //Getter de la id del mejor output
    public int GetMaxOutputId()
    {
        return red.GetMaxOutputId();
    }
}
