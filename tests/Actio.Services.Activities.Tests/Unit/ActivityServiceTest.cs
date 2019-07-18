using System;
using Xunit;
using Actio.Domain.Repositories;
using Moq;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using Actio.Domain.Model;
using Actio.Application.Services;
using Actio.Application.Services.RQ;
using Mapster;

namespace Actio.Services.Activities.Tests.Unit
{
    public class ActivityServiceTest
    {
        [Fact]
        public async Task Activity_Service_Add_Async_Should_Succeed()
        {
            var categoryName = "test";
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(category => 
                category.GetCategory(categoryName)                                        
            ).ReturnsAsync(new Category(categoryName));

            var activityService = new ActivityService(
                  activityRepositoryMock.Object
                , categoryRepositoryMock.Object
            );

            var rq = new ActivityRQ
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Category = categoryName,
                Name = "activity",
                Description = "description",
                CreatedAt = DateTime.UtcNow
            };
            
            await activityService.AddActivity(rq);                                

            categoryRepositoryMock.Verify(
                x => x.GetCategory(categoryName), Times.Once);

            var activity = rq.Adapt<Activity>();
            activity.SetCategory(new Category(categoryName));

            activityRepositoryMock.Verify(
                x => x.Add(It.IsAny<Activity>()), Times.Once
            );
        }
    }
}
