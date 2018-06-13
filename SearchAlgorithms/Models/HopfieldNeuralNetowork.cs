

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms.Models
{
    public class CryptoRandom
    {
        public double RandomValue { get; set; }

        public CryptoRandom()
        {
            using (var p = new RNGCryptoServiceProvider())
            {
                var r = new Random(p.GetHashCode());
                this.RandomValue = r.NextDouble();
            }
        }

    }
    public class Dendrite
    {
        public double Weight { get; set; }

        public Dendrite()
        {
            var n = new CryptoRandom();
            this.Weight = n.RandomValue;
        }
    }
    public class Neuron
    {
        public List<Dendrite> Dendrites { get; set; }
        public double Bias { get; set; }
        public double Delta { get; set; }
        public double Value { get; set; }

        public int DendriteCount
        {
            get
            {
                return Dendrites.Count;
            }
        }

        public Neuron()
        {
            var n = new Random(Environment.TickCount);
            this.Bias = n.NextDouble();

            this.Dendrites = new List<Dendrite>();
        }
    }
    public class Layer
    {
        public List<Neuron> Neurons { get; set; }
        public int NeuronCount
        {
            get
            {
                return Neurons.Count;
            }
        }

        public Layer(int numNeurons)
        {
            Neurons = new List<Neuron>(numNeurons);
        }
    }

    public class NeuralNetwork
    {
        public List<Layer> Layers { get; set; }
        public double LearningRate { get; set; }
        public int LayerCount
        {
            get
            {
                return Layers.Count;
            }
        }

        public NeuralNetwork(double learningRate, int[] layers)
        {
            if (layers.Length < 2) return;

            this.LearningRate = learningRate;
            this.Layers = new List<Layer>();

            for (var l = 0; l < layers.Length; l++)
            {
                var layer = new Layer(layers[l]);
                this.Layers.Add(layer);

                for (var n = 0; n < layers[l]; n++)
                    layer.Neurons.Add(new Neuron());

                layer.Neurons.ForEach((nn) =>
                {
                    if (l == 0)
                        nn.Bias = 0;
                    else
                        for (var d = 0; d < layers[l - 1]; d++)
                            nn.Dendrites.Add(new Dendrite());
                });
            }
        }

        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public double[] Run(List<double> input)
        {
            if (input.Count != this.Layers[0].NeuronCount) return null;

            for (var l = 0; l < Layers.Count; l++)
            {
                var layer = Layers[l];

                for (var n = 0; n < layer.Neurons.Count; n++)
                {
                    var neuron = layer.Neurons[n];

                    if (l == 0)
                        neuron.Value = input[n];
                    else
                    {
                        neuron.Value = 0;
                        for (var np = 0; np < this.Layers[l - 1].Neurons.Count; np++)
                            neuron.Value = neuron.Value + this.Layers[l - 1].Neurons[np].Value * neuron.Dendrites[np].Weight;

                        neuron.Value = Sigmoid(neuron.Value + neuron.Bias);
                    }
                }
            }

            var last = this.Layers[this.Layers.Count - 1];
            var numOutput = last.Neurons.Count;
            var output = new double[numOutput];
            for (var i = 0; i < last.Neurons.Count; i++)
                output[i] = last.Neurons[i].Value;

            return output;
        }

        public bool Train(List<double> input, List<double> output)
        {
            if ((input.Count != this.Layers[0].Neurons.Count) || (output.Count != this.Layers[this.Layers.Count - 1].Neurons.Count)) return false;

            Run(input);

            for (var i = 0; i < this.Layers[this.Layers.Count - 1].Neurons.Count; i++)
            {
                var neuron = this.Layers[this.Layers.Count - 1].Neurons[i];

                neuron.Delta = neuron.Value * (1 - neuron.Value) * (output[i] - neuron.Value);

                for (var j = this.Layers.Count - 2; j > 2; j--)
                {
                    for (var k = 0; k < this.Layers[j].Neurons.Count; k++)
                    {
                        var n = this.Layers[j].Neurons[k];

                        n.Delta = n.Value *
                                  (1 - n.Value) *
                                  this.Layers[j + 1].Neurons[i].Dendrites[k].Weight *
                                  this.Layers[j + 1].Neurons[i].Delta;
                    }
                }
            }

            for (var i = this.Layers.Count - 1; i > 1; i--)
            {
                for (var j = 0; j < this.Layers[i].Neurons.Count; j++)
                {
                    var n = this.Layers[i].Neurons[j];
                    n.Bias = n.Bias + (this.LearningRate * n.Delta);

                    for (var k = 0; k < n.Dendrites.Count; k++)
                        n.Dendrites[k].Weight = n.Dendrites[k].Weight + (this.LearningRate * this.Layers[i - 1].Neurons[k].Value * n.Delta);
                }
            }

            return true;
        }

        public class HopfieldNeuralNetwork
        {
            private int _numInputs;
            ArrayList trainingData = new ArrayList();
            private float[,] weights;
            private float[] tempStorage;
            private float[] inputCells;
            public HopfieldNeuralNetwork(int numInputs)
            {
                _numInputs = numInputs;
                weights = new float[numInputs, numInputs];
                inputCells = new float[numInputs];
                tempStorage = new float[numInputs];
            }
            public void AddTrainingData(float[] data)
            {
                trainingData.Add(data);
            }

            public void Train()
            {
                for (var j = 1; j < _numInputs; j++)
                {
                    for (var i = 0; i < j; i++)
                    {
                        for (var n = 0; n < trainingData.Count; n++)
                        {
                            var data = (float[])trainingData[n];
                            var temp1 = AdjustInput(data[i]) * AdjustInput(data[j]);
                            var temp = Truncate(temp1 + weights[j, i]);
                            weights[i, j] = weights[j, i] = temp;
                        }
                    }
                }
                for (var i = 0; i < _numInputs; i++)
                {
                    tempStorage[i] = 0.0f;
                    for (var j = 0; j < i; j++)
                    {
                        tempStorage[i] += weights[i, j];
                    }
                }
            }

            public float[] Recall(float[] pattern, int numIterations)
            {
                for (var i = 0; i < _numInputs; i++) inputCells[i] = pattern[i];
                for (var ii = 0; ii < numIterations; ii++)
                {
                    for (var i = 0; i < _numInputs; i++)
                    {
                        if (DeltaEnergy(i) > 0.0f)
                        {
                            inputCells[i] = 1.0f;
                        }
                        else
                        {
                            inputCells[i] = -1.0f;
                        }
                    }
                }
                return inputCells;
            }

            private float AdjustInput(float x)
            {
                if (x < 0.0f) return -1.0f;
                return 1.0f;
            }

            private float Truncate(float x)
            {
                //return Math.round(x);
                var i = (int)x;
                return (float)i;
            }

            private float DeltaEnergy(int index)
            {
                var temp = 0.0f;
                for (var j = 0; j < _numInputs; j++)
                {
                    temp += weights[index, j] * inputCells[j];
                }
                return 2.0f * temp - tempStorage[index];
            }


        }
    }
} 