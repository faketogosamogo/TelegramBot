﻿using MediatR;
using Telegram.Bot.Types;
using TelegramBot.ApplicationCore;
using TelegramBot.ApplicationCore.Message.Requests.Commands;
using TelegramBot.ApplicationCore.Requests.Queries;
using TelegramBot.Telegram.Interfaces;

namespace TelegramBot.Telegram.Actions;

public class DefaultAction : IAction
{
    private readonly IMediator _mediator;
    public event Func<Message, Task>? ExecuteDefault;
    
    public DefaultAction(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task ExecuteAsync(Message message)
    {
        Statuses status = (await _mediator.Send(new GetUserCommand(message.Chat.Id))).Status;
        
        await _mediator.Send(new SendMessageCommand(
            Message: BotTextAnswers.DEFAULT,
            ChatId: message.Chat.Id,
            Status: status));
    }
}