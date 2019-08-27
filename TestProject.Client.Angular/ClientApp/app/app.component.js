var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { Person } from './person';
import { PersonService } from './person.Service';
var AppComponent = /** @class */ (function () {
    function AppComponent(dataService) {
        this.dataService = dataService;
        this.selectedPerson = new Person(); // изменяемый товар
        this.tableMode = true; // табличный режим
    }
    AppComponent.prototype.ngOnInit = function () {
        this.loadProducts(); // загрузка данных при старте компонента  
    };
    // получаем данные через сервис
    AppComponent.prototype.loadProducts = function () {
        var _this = this;
        this.dataService.getPersons()
            .subscribe(function (data) { return _this.persons = data; });
    };
    AppComponent = __decorate([
        Component({
            selector: 'app',
            templateUrl: './app.component.html',
            providers: [PersonService]
        }),
        __metadata("design:paramtypes", [PersonService])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
//# sourceMappingURL=app.component.js.map