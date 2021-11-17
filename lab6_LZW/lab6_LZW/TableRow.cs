using System;
using System.Collections.Generic;
using System.Text;

namespace lab6_LZW
{
    class TableRow
    {
        public List<byte> row;

        public byte this[int i]
        {
            get { return row[i]; }
        }

        public TableRow()
        {
            row = new List<byte>();
        }

        public TableRow(int i)
        {
            row = new List<byte>() { (byte)i};
        }

        public static TableRow operator +(TableRow a, TableRow b)
        {
            var res = new TableRow();
            res.row.AddRange(a.row);
            res.row.AddRange(b.row);
            return res;
        }

        public static TableRow operator +(TableRow a, byte b)
        {
            var res = new TableRow();
            res.row.AddRange(a.row);
            res.row.Add(b);
            return res;
        }

        public bool Equals(TableRow obj)
        {
            if (obj == null || obj.row.Count != this.row.Count)
                return false;

            for (int i = 0; i < this.row.Count; i++)
                if (obj.row[i] != this.row[i])
                    return false;

            return true;
        }

        public override string ToString()
        {
            return String.Join(' ', row);
        }
    }
}
