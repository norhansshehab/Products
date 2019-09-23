import { Component, OnInit } from '@angular/core';
import { Feature } from 'src/app/Project/Models/feature';
import { FeatureService } from 'src/app/Project/Services/feature.service';

@Component({
  selector: 'app-features',
  templateUrl: './features.component.html',
  styleUrls: ['./features.component.css']
})
export class FeaturesComponent implements OnInit {

  features: Feature[] = [];
  newfeature: Feature = new Feature(0, "");
  flag: boolean;

  constructor(private FeatureServiceObject: FeatureService) { }

  ngOnInit() {
    this.GetAll();
  }

  GetAll() {
    this.FeatureServiceObject.GetAllFeatures().subscribe(a => this.features = a, err => console.log(err))
  }


  DeleteFeature(id: number) {
    const index = this.features.findIndex(f => f.FeatureID === id);
    this.FeatureServiceObject.Delete(id).subscribe(a => this.features.splice(index, 1));
  }


  DetailsFeature(id: number) {
    this.flag = true;
    this.FeatureServiceObject.GetFeatureById(id).subscribe(a => this.newfeature = a)
  }


  AddNewFeature() {
    this.FeatureServiceObject.CreateFeature(this.newfeature).subscribe(a => this.features.push(a))
  }


  UpdateFeature() {
    const index = this.features.indexOf(this.newfeature);
    this.FeatureServiceObject.EditFeature(this.newfeature.FeatureID,this.newfeature).subscribe(a => this.features.splice(index, 1, this.newfeature))
  }

}
