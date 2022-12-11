import { EventEmitter, Type } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { eLayoutType } from '../enums/common';
import { Config } from './config';
import { NgxsStoragePluginOptions } from '@ngxs/storage-plugin';

export namespace ABP {
  export interface Root {
    environment: Partial<Config.Environment>;
    skipGetAppConfiguration?: boolean;
    sendNullsAsQueryParam?: boolean;
    cultureNameLocaleFileMap?: Dictionary<string>;
    ngxsStoragePluginOptions?: NgxsStoragePluginOptions & { key?: string[] };
  }

  export interface Test {
    baseHref?: Router;
  }

  export type PagedResponse<T> = {
    totalCount: number;
  } & PagedItemsResponse<T>;

  export interface PagedItemsResponse<T> {
    items: T[];
  }

  export interface PageQueryParams {
    filter?: string;
    sorting?: string;
    skipCount?: number;
    maxResultCount?: number;
  }

  export interface Lookup {
    id: string;
    displayName: string;
  }

  export interface Nav {
    name: string;
    parentName?: string;
    requiredPolicy?: string;
    order?: number;
    invisible?: boolean;
  }

  export interface Route extends Nav {
    path: string;
    layout?: eLayoutType;
    iconClass?: string;
  }

  export interface Tab extends Nav {
    component: Type<any>;
  }

  export interface BasicItem {
    id: string;
    name: string;
  }

  export interface Option<T> {
    key: Extract<keyof T, string>;
    value: T[Extract<keyof T, string>];
  }

  export interface Dictionary<T = any> {
    [key: string]: T;
  }

  export type ExtractFromOutput<
    T extends EventEmitter<any> | Subject<any>
  > = T extends EventEmitter<infer X> ? X : T extends Subject<infer Y> ? Y : never;
}