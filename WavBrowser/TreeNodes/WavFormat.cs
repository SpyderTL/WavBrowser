using System.IO;
using System.Text;

namespace WavBrowser.TreeNodes
{
	public class WavFormat : DataNode
	{
		public IReadable Source;
		public long Position;

		public WavFormat() : base(false)
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

				var format = reader.ReadUInt16();
				var channelCount = reader.ReadUInt16();

				return new
				{
					Format = format,
					ChannelCount = channelCount
				};
			}
		}
	}
}