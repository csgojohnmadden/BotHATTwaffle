﻿using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace BotHATTwaffle.Modules
{
	/// <summary>
	/// Contains commands which provide links to various Source development tools.
	/// TODO: Look into creating a generic class which can build these kinds of commands from JSON data.
	/// </summary>
	public class ToolsModule : ModuleBase<SocketCommandContext>
	{
		[Command("VTFEdit")]
		[Summary("`>VTFEdit` Provides a download link to VTFEdit.")]
		[Alias("vtf")]
		public async Task VTFEditAsync()
		{
			var embed = new EmbedBuilder
			{
				Author = new EmbedAuthorBuilder
				{
					Name = "Download VTFEdit",
					IconUrl = "https://cdn.discordapp.com/icons/111951182947258368/0e82dec99052c22abfbe989ece074cf5.png"
				},
				Title = "Click Here",
				Url = "https://www.tophattwaffle.com/downloads/vtfedit/",
				ThumbnailUrl = "https://content.tophattwaffle.com/BotHATTwaffle/vtfedit.png",
				Color = new Color(255, 206, 199),
				Description = "VTFEdit is a lightweight tool used to convert images into Valve's proprietary format - VTF " +
				              "(Valve Texture Format). Because it has a GUI, it is substantially easier to use than Valve's " +
				              "own CLI tool, VTEX (Valve Texture Tool)."
			};

			await ReplyAsync(string.Empty, false, embed.Build());
		}

		[Command("GCFScape")]
		[Summary("`>GCFScape` Provides a download link to GCFScape.")]
		[Alias("gcf")]
		public async Task GCFScapeAsync()
		{
			var embed = new EmbedBuilder
			{
				Author = new EmbedAuthorBuilder
				{
					Name = "Download GCFScape",
					IconUrl = "https://cdn.discordapp.com/icons/111951182947258368/0e82dec99052c22abfbe989ece074cf5.png"
				},
				Title = "Click Here",
				Url = "https://www.tophattwaffle.com/downloads/gcfscape/",
				ThumbnailUrl = "https://content.tophattwaffle.com/BotHATTwaffle/gcfscape.png",
				Color = new Color(63, 56, 156),
				Description = "GCFScape is a tool for exploring, extracting, and managing content in various package formats " +
				              "used by Valve and Steam. Supported formats include VPK, GCF, PAK, BSP, and more."
			};

			await ReplyAsync(string.Empty, false, embed.Build());
		}

		[Command("VMTEditor")]
		[Summary("`>VMTEditor` Provides a download link to VMT Editor.")]
		[Alias("vmt")]
		public async Task VMTEditorAsync()
		{
			var embed = new EmbedBuilder
			{
				Author = new EmbedAuthorBuilder
				{
					Name = "Download VMT Editor",
					IconUrl = "https://cdn.discordapp.com/icons/111951182947258368/0e82dec99052c22abfbe989ece074cf5.png"
				},
				Title = "Click Here",
				Url = "https://gira-x.github.io/VMT-Editor/",
				ThumbnailUrl = "https://content.tophattwaffle.com/BotHATTwaffle/vmteditor.png",
				Color = new Color(50, 50, 50),
				Description = "VMT Editor is, hands down, one of the best VMT (Valve Material Type) creation tools that " +
				              "exists for the Source engine. It quickly became a standard tool for most designers that " +
				              "regularly create VMT files. Created by Yanzl over at MapCore."
			};

			await ReplyAsync(string.Empty, false, embed.Build());
		}

		[Command("VIDE")]
		[Summary("`>VIDE` Provides a download link to VIDE.")]
		public async Task VIDEAsync()
		{
			var embed = new EmbedBuilder
			{
				Author = new EmbedAuthorBuilder
				{
					Name = "Download VIDE",
					IconUrl = "https://cdn.discordapp.com/icons/111951182947258368/0e82dec99052c22abfbe989ece074cf5.png"
				},
				Title = "Click Here",
				Url = "https://www.tophattwaffle.com/downloads/vide/",
				ThumbnailUrl = "https://content.tophattwaffle.com/BotHATTwaffle/vide.png",
				Color = new Color(50, 50, 50),
				Description = "VIDE (Valve Integrated Development Environment) is a 3rd-party program which contains various " +
				              "tools. It is popular for its pakfile lump editor (packing assets into a level), but it can do " +
				              "so much more than that."
			};

			await ReplyAsync(string.Empty, false, embed.Build());
		}

		[Command("WallWorm")]
		[Summary("`>WallWorm` Provides a link to Wall Worm's website.")]
		[Alias("wwmt")]
		public async Task WallWormAsync()
		{
			var embed = new EmbedBuilder
			{
				Author = new EmbedAuthorBuilder
				{
					Name = "Check out Wall Worm",
					IconUrl = "https://cdn.discordapp.com/icons/111951182947258368/0e82dec99052c22abfbe989ece074cf5.png"
				},
				Title = "Click Here",
				Url = "https://dev.wallworm.com/",
				ThumbnailUrl = "https://content.tophattwaffle.com/BotHATTwaffle/worm_logo.png",
				Color = new Color(21, 21, 21),
				Description = "Wall Worm tools enable developers to design assets and level in Autodesk's 3ds Max and export " +
				              "them into the Source Engine. It's the best thing to ever happen to Source Engine modelling."
			};

			await ReplyAsync(string.Empty, false, embed.Build());
		}

		[Command("BSPSource")]
		[Summary("`>BSPSource` Provides a download link to BSPSource.")]
		[Alias("bsp")]
		public async Task BSPSourceAsync()
		{
			var embed = new EmbedBuilder
			{
				Author = new EmbedAuthorBuilder
				{
					Name = "Download BSPSource",
					IconUrl = "https://cdn.discordapp.com/icons/111951182947258368/0e82dec99052c22abfbe989ece074cf5.png"
				},
				Title = "Click Here",
				Url = "https://www.tophattwaffle.com/downloads/bspsource/",
				ThumbnailUrl = "https://content.tophattwaffle.com/BotHATTwaffle/BSPSource_icon.png",
				Color = new Color(84,137,71),
				Description = "BSPSource is a tool for decompiling Source's BSP (Binary Space Partition) files into VMF " +
				              "(Valve Map Format) files that can be opened with Hammer. It is a great tool to see how things " +
				              "are done in a map. It should not be used to steal content."
			};

			await ReplyAsync(string.Empty, false, embed.Build());
		}

		[Command("log")]
		[Summary("`>log` Provides a link to the compile log checker on Interlopers.")]
		[Alias("l")]
		public async Task LogCheckerAsync()
		{
			var embed = new EmbedBuilder
			{
				Author = new EmbedAuthorBuilder
				{
					Name = "Interlopers Compile Log Checker",
					IconUrl = "https://cdn.discordapp.com/icons/111951182947258368/0e82dec99052c22abfbe989ece074cf5.png"
				},
				Title = "Click Here",
				Url = "http://www.interlopers.net/errors",
				ThumbnailUrl = "https://www.tophattwaffle.com/wp-content/uploads/2017/12/selectall.jpg",
				Color = new Color(84, 137, 71),
				Description = "The compile log checker on Interlopers is a web tool which analyses compile logs of maps to " +
				              "detect compilation issues and propose solutions. Simply copy and paste an entire log or a " +
				              "section with an error and click the button to have the log checked."
			};

			await ReplyAsync(string.Empty, false, embed.Build());
		}
	}
}
