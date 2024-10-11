import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProfileService } from 'src/app/services/Profile.service';
import { Profile } from 'src/app/models/profile';

@Component({
  selector: 'app-profile-details',
  standalone: true,
  templateUrl: './profile-details.component.html',
  styleUrls: ['./profile-details.component.css'],
})
export class ProfileDetailsComponent implements OnInit {
  profile: Profile | null = null;

  constructor(private route: ActivatedRoute, private profileService: ProfileService) {}

  ngOnInit(): void {
    const profileId = this.route.snapshot.params['id'];
    this.loadProfile(profileId);
  }

  loadProfile(id: number): void {
    this.profileService.getProfileById(id).subscribe((data) => {
      this.profile = data;
    });
  }
}
