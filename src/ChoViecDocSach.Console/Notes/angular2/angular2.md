<!-- TOC insertAnchor:true orderedList:true -->

1. [Angular 2](#angular-2)
    1. [1. use Typescript: strongly typed](#1-use-typescript-strongly-typed)
    2. [2. npm (Node package manager)](#2-npm-node-package-manager)
        1. [AngularCli is a boilerplate for angular](#angularcli-is-a-boilerplate-for-angular)
    3. [3. module is namespace, to organize code](#3-module-is-namespace-to-organize-code)
    4. [4. component is a template (for view) + metadata using Decorator](#4-component-is-a-template-for-view-metadata-using-decorator)
    5. [5. Template](#5-template)

<!-- /TOC -->

<a id="markdown-angular-2" name="angular-2"></a>
# Angular 2
<a id="markdown-1-use-typescript-strongly-typed" name="1-use-typescript-strongly-typed"></a>
## 1. use Typescript: strongly typed 
        Typescript type definition files (*.d.ts)
        And using object !
<a id="markdown-2-npm-node-package-manager" name="2-npm-node-package-manager"></a>
## 2. npm (Node package manager)
<a id="markdown-angularcli-is-a-boilerplate-for-angular" name="angularcli-is-a-boilerplate-for-angular"></a>
### AngularCli is a boilerplate for angular 
<a id="markdown-" name=""></a>
<a id="markdown-3-module-is-namespace-to-organize-code" name="3-module-is-namespace-to-organize-code"></a>
## 3. module is namespace, to organize code 
   how to use? ES 2015 
    1. create code file: product.ts
            export class Product { //
                
            }
    2. and another file: product-list.ts
            import { Product } from
            '.product'
<a id="markdown-4-component-is-a-template-for-view-metadata-using-decorator" name="4-component-is-a-template-for-view-metadata-using-decorator"></a>
## 4. component is a template (for view) + metadata using Decorator
    Decorator is a function that adds metadata to a class, its members, or its method arguments
    prefixed with an @
    Comment: this decorator @Component is right above the export class like attribute in c#
    we have view layout, binding and directive name used in html
    Component = Template + Class + Metadaa

Here is the example:
![component example](Component.png)
    
<a id="markdown-5-template" name="5-template"></a>
## 5. Template

define a template
![Define a template](Define_a_template.png)

filter
![filter 1](Template_filter_list.png)

product page
![Product page](Product.png)

a component with directive
![awholecomponent](awholecomponent.png)
        