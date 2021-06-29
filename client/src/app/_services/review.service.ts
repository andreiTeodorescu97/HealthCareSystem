import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddReviewDto } from 'app/_models/_reviews/AddReviewDto';
import { ReviewDto } from 'app/_models/_reviews/ReviewDto';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addReview(model: AddReviewDto) {
    return this.http.post(this.baseUrl + 'review/add', model);
  }

  deleteReview(reviewId: number) {
    return this.http.delete(this.baseUrl + 'review/delete?reviewId=' + reviewId);
  }

  getReviews(){
    return this.http.get<ReviewDto[]>(this.baseUrl + 'review/get');
  }

  getReviewsForDoctor(doctorId: number){
    return this.http.get<ReviewDto[]>(this.baseUrl + 'review/get-doctor-reviews?doctorId=' + doctorId);
  }
}
