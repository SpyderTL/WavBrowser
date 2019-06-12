using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavBrowser.TreeNodes
{
	public class WavFile : DataNode, IReadable
	{
		public string Path;

		public WavFile() : base(true)
		{
		}

		public override void Reload()
		{
			Nodes.Clear();

			using (var reader = new BinaryReader(GetStream()))
			{
				Nodes.Add(new WavHeader { Text = "Header", Source = this });

				reader.BaseStream.Position = 12;

				while (reader.BaseStream.Position < reader.BaseStream.Length)
				{
					var position = reader.BaseStream.Position;

					var groupID = Encoding.ASCII.GetString(reader.ReadBytes(4));
					var length = reader.ReadUInt32();

					switch (groupID)
					{
						case "fmt ":
							Nodes.Add(new WavFormat { Text = "Format", Position = position, Source = this });
							break;

						case "data":
							Nodes.Add(new WavData { Text = "Data", Position = position, Source = this });
							break;

						default:
							Nodes.Add(groupID);
							break;
					}

					reader.BaseStream.Seek(length, SeekOrigin.Current);
				}
			}
		}

		public override object GetProperties()
		{
			return new
			{
				Path,
				new FileInfo(Path).Length
			};
		}

		public Stream GetStream()
		{
			return File.OpenRead(Path);
		}
	}
}
