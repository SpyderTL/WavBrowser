using System.IO;
using System.Text;

namespace WavBrowser.TreeNodes
{
	public class WavHeader : DataNode
	{
		public IReadable Source;

		public WavHeader() : base(false)
		{
		}

		public override void Reload()
		{
		}

		public override object GetProperties()
		{
			using (var reader = new BinaryReader(Source.GetStream()))
			{
				var groupID = Encoding.ASCII.GetString(reader.ReadBytes(4));
				var fileLength = reader.ReadUInt32();
				var type = Encoding.ASCII.GetString(reader.ReadBytes(4));

				return new
				{
					GroupID = groupID,
					FileLength = fileLength,
					Type = type
				};
			}
		}
	}
}