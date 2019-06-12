using System.IO;
using System.Text;

namespace WavBrowser.TreeNodes
{
	public class WavData : DataNode
	{
		public IReadable Source;
		public long Position;

		public WavData() : base(false)
		{
		}

		public override void Reload()
		{
		}

		public override object GetProperties()
		{
			using (var reader = new BinaryReader(Source.GetStream()))
			{
				reader.BaseStream.Position = Position;

				var groupID = Encoding.ASCII.GetString(reader.ReadBytes(4));
				var length = reader.ReadUInt32();

				return new
				{
					Data = reader.ReadBytes((int)length)
				};
			}
		}
	}
}