// Decompiled with JetBrains decompiler
// Type: Penguin.Random.Interfaces.IRandomGenerator
// Assembly: Penguin.Random, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A5518E0-DEF6-4913-9205-059E59B1CA91
// Assembly location: C:\Git\MassageEnvy.com Umbraco 8\MassageEnvy\bin\Penguin.Random.dll

namespace Penguin.Random.Interfaces
{
    /// <summary>An interface used to define RNG extension methods</summary>
    public interface IRandomGenerator
    {
        /// <summary>Gets the next ulong from the RNG</summary>
        /// <returns></returns>
        ulong Next();

        /// <summary>Gets the next ulong from the RNG</summary>
        /// <returns></returns>
        ulong Next(ulong min, ulong max);

        /// <summary>Gets the next double from the RNG</summary>
        /// <returns></returns>
        double NextDouble();
    }
}
