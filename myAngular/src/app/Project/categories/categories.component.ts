import { Component, OnInit } from '@angular/core';
import { Category } from '../Models/category';
import { CategoryService } from '../Services/category.service';
import { Feature } from '../Models/feature';
import { FeatureService } from '../Services/feature.service';
import { CategoryFeature } from '../Models/category-feature';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})

export class CategoriesComponent implements OnInit {

  categories: Category[] = [];
  newCategory: Category = new Category(0, "");
  flag: boolean;
  //Selected: boolean;

  features: Feature[] = [];
  //CategoryFeatures : CategoryFeature[] = [];
  
  constructor(private CategoryServiceObject: CategoryService, private FeatureServiceObject: FeatureService) { }

  ngOnInit() {
    this.GetAll();
    this.FeatureServiceObject.GetAllFeatures().subscribe(a => this.features = a, err => console.log(err))
  }

  GetAll() {
    this.CategoryServiceObject.GetAllCategories().subscribe(a => this.categories = a, err => console.log(err))
  }


  DeleteCategory(id: number) {
    let index = this.categories.findIndex(f => f.CatID === id);
    this.CategoryServiceObject.Delete(id).subscribe(a => this.categories.splice(index, 1));
  }


  DetailsCategory(id: number) {
    this.flag = true;
    this.CategoryServiceObject.GetCategoryById(id).subscribe(a => this.newCategory = a)
  }


  AddNewCategory() {
    this.CategoryServiceObject.CreateCategory(this.newCategory).subscribe(a => this.categories.push(a))
  }


  UpdateCategory() {
    let index = this.categories.indexOf(this.newCategory);
    this.CategoryServiceObject.EditCategory(this.newCategory.CatID,this.newCategory).subscribe(a => this.categories.splice(index, 1, this.newCategory))
  }

  // InsertChkList(FID, Selected) {
  //   debugger;
  //   let index = this.CategoryFeatures.indexOf(FID);
  //   console.log("index = "+index);
  //   console.log("Selected = "+Selected);

  //   if (Selected && index == -1) {
  //     this.CategoryFeatures.push({CatID: this.newCategory.CatID ,FeatureID: FID });
  //     //this.Selected = false; 
  //     console.log("Push = "+this.CategoryFeatures);
  //   }

  //   else {
  //     this.CategoryFeatures.splice(index, 1);
  //     console.log("splice = "+this.CategoryFeatures);
  //   }
  // }
  InsertChkList(Selected, event) {
    debugger;
          console.log("Selected = "+Selected);
          console.log("event = "+event);

  }
}
