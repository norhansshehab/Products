import { Component, OnInit } from '@angular/core';
import { Product } from '../Models/product';
import { ProductService } from '../Services/product.service';
import { Category } from '../Models/category';
import { CategoryService } from '../Services/category.service';
import { Feature } from '../Models/feature';
import { ProductFeatureValue } from '../Models/product-feature-value';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products: Product[] = [];
  newproduct: Product = new Product(0, "");
  flag: boolean;

  categories: Category[] = [];
  features: Feature[] = [];
  ProductFeatureValues: ProductFeatureValue[] = [];


  constructor(private ProductServiceObject: ProductService, private CategoryServiceObject: CategoryService) { }

  ngOnInit() {
    this.GetAll();
    this.CategoryServiceObject.GetAllCategories().subscribe(c => this.categories = c);
    //this.newproduct.ProductFeatureValues = this.ProductFeatureValues;

  }


  GetAll() {
    this.ProductServiceObject.GetAllProducts().subscribe(a => this.products = a, err => console.log(err))
  }


  DeleteProduct(id: number) {
    let index = this.products.findIndex(f => f.CatID === id);
    this.ProductServiceObject.Delete(id).subscribe(a => this.products.splice(index, 1));
  }


  DetailsProduct(id: number) {
    this.flag = true;
    // this.newproduct.ProductFeatureValues = this.ProductFeatureValues;

    this.ProductServiceObject.GetProductById(id).subscribe(a => {
      this.newproduct = a;
      this.ProductFeatureValues = a.ProductFeatureValues;
    })
  }


  AddNewProduct() {
    this.ProductServiceObject.CreateProduct(this.newproduct).subscribe(a => this.products.push(a))
  }


  UpdateProduct() {
    let index = this.products.indexOf(this.newproduct);
    this.ProductServiceObject.EditProduct(this.newproduct).subscribe(a => this.products.splice(index, 1, this.newproduct))
  }

  GetCategoryFeatures(SelectedCatID: number) {

    this.ProductServiceObject.GetFeaturesForCategory(SelectedCatID).subscribe(result => {
      // debugger;
      this.newproduct.ProductFeatureValues = this.ProductFeatureValues;

      this.features = result;


      if (this.newproduct.ProdID == null) {
        this.newproduct.ProductFeatureValues = [];
      }

      result.forEach(element => {
        this.newproduct.ProductFeatureValues.push({ 'ProdID': this.newproduct.ProdID, 'FeatureID': element.FeatureID, 'Value': null });
      })


    })
  }

  AddValue (pid, fid) {
    debugger;
    // this.newproduct.ProductFeatureValues = this.ProductFeatureValues;

   // if(pid != null && fid != null){
      this.newproduct.ProductFeatureValues.push({ 'ProdID': pid, 'FeatureID': fid, 'Value': null });
   //   return false;
    // }
    // return true;
  };

  RemoveValue (pf, PFV_idx) {
    // this.newproduct.ProductFeatureValues = this.ProductFeatureValues;
    let index = this.newproduct.ProductFeatureValues.indexOf(pf);

    if (PFV_idx != 0) {
      this.newproduct.ProductFeatureValues.splice(index, 1);
      // return false;
    }
    // return true;

  };


}
