using CSharpFunctionalExtensions;
using MediatR;
using MindFactory.News.Application.Common.Responses;

namespace MindFactory.News.Application.Authors.Commands.AddAuthor
{
    public class AddAuthorCommand : IRequest<Result<SingleResponse>>
    {
        public string Name { get; set; }

        public static AddAuthorCommand CreateFrom(AddAuthorRequest request)
        {
            return new AddAuthorCommand
            {
                Name = request.Name
            };
        }
    }
}