using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation
{
    public class DockingDataParser
    {
        public static (Memory.ValueMask Mask, ulong Address, ulong Value) ParseV1(string input)
        {
            var parts = input.Split(" = ");
            if (parts[0] == "mask")
            {
                return (new Memory.ValueMask(parts[1]), default, default);
            }

            var val = parts[0][(parts[0].IndexOf('[')+1)..^1];
            var address = ulong.Parse(val);
            return (default, address, ulong.Parse(parts[1]));
        }

        public static (Memory.AddressMask Mask, ulong Address, ulong Value) ParseV2(string input)
        {
            var parts = input.Split(" = ");
            if (parts[0] == "mask")
            {
                return (new Memory.AddressMask(parts[1]), default, default);
            }

            var val = parts[0][(parts[0].IndexOf('[')+1)..^1];
            var address = ulong.Parse(val);
            return (default, address, ulong.Parse(parts[1]));
        }
    }

    public class Memory
    {
        public IDictionary<ulong, ulong> MemoryContents { get; } = new Dictionary<ulong, ulong>();

        public ulong ApplyValueV1(ulong address, ValueMask valueMask, ulong memoryValue)
        {
            var value = valueMask.ApplyOn(memoryValue);
            MemoryContents[address] = value;
            return value;
        }

        public ulong[] ApplyValueV2(ulong address, AddressMask mask, ulong value)
        {
            var addressWildcard = mask.GetAddressWildcard(address);
            var allAddresses = CalculateAllAddresses(addressWildcard);
            foreach (var calculatedAddress in allAddresses)
            {
                MemoryContents[calculatedAddress] = value;
            }

            return allAddresses;
        }

        private ulong[] CalculateAllAddresses(string addressWildcard)
        {
            var indexOfFirstWildcard = addressWildcard.IndexOf('X');
            if (indexOfFirstWildcard == -1) return new[] {Convert.ToUInt64(addressWildcard, 2)};

            return new[] {'0', '1'}
                .SelectMany(fix => CalculateAllAddresses(FixWildcard(addressWildcard, indexOfFirstWildcard, fix)))
                .ToArray();
            }

        private string FixWildcard(string addressWildcard, in int indexOfFirstWildcard, in char fixChar)
        {
            return addressWildcard.Substring(0, indexOfFirstWildcard) + fixChar + addressWildcard.Substring(indexOfFirstWildcard + 1);
        }

        public class AddressMask
        {
            private readonly string _mask;

            public AddressMask(string mask)
            {
                _mask = mask;
            }

            public string GetAddressWildcard(ulong address)
            {
                var sb = new StringBuilder();
                var binaryAddress = Convert.ToString((long) address, 2).PadLeft(36, '0');
                for (var i = 0; i < _mask.Length; i++)
                {
                    sb.Append(_mask[i] switch
                    {
                        '0' => binaryAddress[i],
                        '1' => '1',
                        'X' => 'X',
                        _ => throw new ArgumentOutOfRangeException()
                    });
                }

                return sb.ToString();
            }

            public static implicit operator AddressMask(string value) => new AddressMask(value);
        }

        public class ValueMask
        {
            private readonly ulong _orMask = 0;
            private readonly ulong _andMask = 68719476735UL;

            public string MaskString { get; }
            public string OrString => Convert.ToString((long) _orMask, 2).PadLeft(36, '0');
            public string AndString => Convert.ToString((long) _andMask, 2).PadLeft(36, '0');

            public ValueMask(string value)
            {
                MaskString = value;
                for (var bitPosition = value.Length - 1; bitPosition >= 0; bitPosition--)
                {
                    var bitChar = value[bitPosition];
                    var power = 35 - bitPosition;
                    var bitValue = (ulong) 1 << power;
                    if (bitChar == '0') _andMask -= bitValue;
                    if (bitChar == '1') _orMask += bitValue;
                }
            }

            public override string ToString()
            {
                return $"And: {AndString} / Or: {OrString}";
            }

            public ulong ApplyOn(ulong value)
            {
                return value & _andMask | _orMask;
            }

            public static implicit operator ValueMask(string value) => new ValueMask(value);
        }
    }
}