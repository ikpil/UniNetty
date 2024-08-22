// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics.CodeAnalysis;

namespace UniNetty.Logging
{
    public readonly struct EventId : IEquatable<EventId>
    {
        public readonly int Id;
        public readonly string Name;

        public EventId(int id, string name = null)
        {
            Id = id;
            Name = name;
        }

        public bool Equals(EventId other)
        {
            return Id == other.Id;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is EventId eventId && Equals(eventId);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return Name ?? Id.ToString();
        }

        public static implicit operator EventId(int i)
        {
            return new EventId(i);
        }

        public static bool operator ==(EventId left, EventId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EventId left, EventId right)
        {
            return !left.Equals(right);
        }
    }
}