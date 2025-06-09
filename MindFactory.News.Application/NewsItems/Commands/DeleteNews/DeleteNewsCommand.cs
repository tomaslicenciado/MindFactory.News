using CSharpFunctionalExtensions;
using MediatR;
using MindFactory.News.Application.Common.Responses;

namespace MindFactory.News.Application.NewsItems.Commands.DeleteNews
{
    public class DeleteNewsCommand : IRequest<Result<SingleResponse>>
    {
        public int Id { get; set; }

        public static DeleteNewsCommand CreateFrom(int id)
        {
            return new()
            {
                Id = id
            };
        }
    }
}