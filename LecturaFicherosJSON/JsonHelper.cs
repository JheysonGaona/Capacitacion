using System;
using UnityEngine;

namespace Capacitacion {

    public static class JsonHelper {

        public static string ToJson<T>(T[] array) {
            Pregunta<T> pregunta = new Pregunta<T>();
            pregunta.Preguntas = array;
            return JsonUtility.ToJson(pregunta);
        }

        public static T[] FromJson<T>(string json) {
        Pregunta<T> pregunta = JsonUtility.FromJson<Pregunta<T>>(json);
        return pregunta.Preguntas;
    }

        [Serializable]
        private class Pregunta<T> {
            public T[] Preguntas;
        }
    }
}