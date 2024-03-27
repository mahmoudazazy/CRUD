import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { UserRouteAccessService } from './route-guard/user-route-access-service';
import { AuthService } from './auth.service';
// import { UsersService } from './users.service';
import { HttpClientModule } from '@angular/common/http';
// import { RequirePermissionsDirective } from './directives/require-permissions.directive';



@NgModule({
    // declarations: [RequirePermissionsDirective],
    providers: [
        // UserRouteAccessService,
        AuthService
        //  , UsersService
    ],
    imports: [
        CommonModule,
        HttpClientModule
    ],
    exports: [
        //RequirePermissionsDirective
    ]
})

export class ServicesModule { }
