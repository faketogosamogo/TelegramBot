﻿using Telegram.Bot;
using TelegramBot.ApplicationCore.Entities;
using TelegramBot.ApplicationCore.Interfaces;

namespace TelegramBot.Telegram.Common;

public class PictureDownloader : IPictureDownloader
{
    private readonly ITelegramBotClient _botClient;
    private readonly IPictureRepository _pictureRepository;

    public PictureDownloader(ITelegramBotClient botClient, IPictureRepository pictureRepository)
    {
        _botClient = botClient;
        _pictureRepository = pictureRepository;
    }

    public async Task<Picture> DownloadAsync(string picId, string? caption, User user)
    {
        var tempPicId = picId.GetHashCode();
        var path = (await _botClient.GetFileAsync(picId)).FilePath!;
        string picPath = await _pictureRepository
                             .GeneratePathAsync(user.Id) + tempPicId + ".jpg";
        
        await using (FileStream stream = File.Create(picPath))
        {
            await _botClient.DownloadFileAsync(
                filePath: path,
                destination: stream
            );
        }

        return new Picture(
            path: picPath,
            caption: caption)
        {
            Id = tempPicId,
            User = user
        };

    }
    
}