﻿// Decompiled with JetBrains decompiler
// Type: Penguin.Random.Prng.FullCycle.Xoroshiro64
// Assembly: Penguin.Random, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A5518E0-DEF6-4913-9205-059E59B1CA91
// Assembly location: C:\Git\MassageEnvy.com Umbraco 8\MassageEnvy\bin\Penguin.Random.dll

using Penguin.Random.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace Penguin.Random.Prng.FullCycle
{
    /// <summary>And implementation of a Xoroshiro64 RNG</summary>
    public class Mulberry32  : IRandomGenerator<UInt32>
    {
        private uint _seed;

        /// <summary>
        /// Creates an instance of this RNG using the given state as the starting point
        /// </summary>
        public Mulberry32 (uint seed)
        {
            _seed = seed;
        }

        /// <summary>
        /// Creates an instance of this RNG using the given state as the starting point
        /// </summary>
        public Mulberry32()
        {
            System.Random r = new System.Random();

            _seed = unchecked((UInt32)r.Next());
        }
        /// <summary>
        /// Creates an instance of this RNG using the given state as the starting point
        /// </summary>
        /// <param name="state"></param>
        public Mulberry32 (State state)
        {
            this._seed = state._seed;
        }

        /// <summary>
        /// Returns the current machine state so it can be persisted and loaded
        /// </summary>
        /// <returns>The current Machine state</returns>
        public Mulberry32 .State GetState()
        {
            return new Mulberry32 .State()
            {
                _seed = this._seed,
            };
        }

        /// <summary>Get the next ulong for this instance.</summary>
        /// <returns>Next psuedo-random value.</returns>
        public uint Next()
        {
            uint z = _seed += 0x6D2B79F5;
            z = (z ^ z >> 15) * (1 | z);
            z ^= z + (z ^ z >> 7) * (61 | z);
            return z ^ z >> 14;
        }

        /// <summary>Get the next ulong for this instance.</summary>
        /// <returns>Next psuedo-random value.</returns>
        public UInt32 Next(UInt32 min, UInt32 max)
        {

            uint z = Next();

            while(z < min || z >= max)
            {
                z = Next();
            }

            return z;
        }

        /// <summary>
        /// Returns the next double from the sequence generated by taking the state and dividing it by ulong.MaxValue
        /// </summary>
        /// <returns>The next double from the sequence created from the next ulong</returns>
        public double NextDouble()
        {
            return this.Next() / UInt32.MaxValue;
        }

        /// <summary>The previous machine state used for saving/loading</summary>
        public class State
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            public UInt32 _seed;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
    }
}
