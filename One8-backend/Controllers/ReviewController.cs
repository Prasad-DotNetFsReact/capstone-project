using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using One8_backend.Models;
using One8_backend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One8_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByRestaurantId(int restaurantId)
        {
            var reviews = await _reviewRepository.GetReviewsByRestaurantIdAsync(restaurantId);
            return Ok(reviews);
        }

        [HttpPost]
        //[Authorize (Roles ="Customer")]
        public async Task<ActionResult> AddReview([FromBody] Review review)
        {
            await _reviewRepository.AddReviewAsync(review);
            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview(int id, [FromBody] Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            await _reviewRepository.UpdateReviewAsync(review);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Customer")]

        public async Task<ActionResult> SoftDeleteReview(int id)
        {
            await _reviewRepository.SoftDeleteReviewAsync(id);
            return NoContent();
        }
    }
}