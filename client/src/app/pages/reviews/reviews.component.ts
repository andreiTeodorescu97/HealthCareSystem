import { Component, OnInit } from '@angular/core';
import { ReviewDto } from 'app/_models/_reviews/ReviewDto';
import { ReviewService } from 'app/_services/review.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {

  reviews: ReviewDto[];

  constructor(private reviewService: ReviewService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getReviews();
  } 

  getReviews(){
    this.reviewService.getReviews().subscribe(data => {
      this.reviews = data;
    })
  }

  deleteReview(reviewId: number){
    this.reviewService.deleteReview(reviewId).subscribe(() => {
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Recenzia a fost stearsa cu succes!</span>',
        "Status",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.getReviews();
    });
  }

}
