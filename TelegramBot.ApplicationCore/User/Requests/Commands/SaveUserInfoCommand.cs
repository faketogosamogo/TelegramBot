﻿using MediatR;

namespace TelegramBot.ApplicationCore.Requests.Commands;

public record SaveUserInfoCommand(long UserId) : IRequest;