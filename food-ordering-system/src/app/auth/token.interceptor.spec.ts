import {TestBed} from '@angular/core/testing';

import {tokenInterceptor} from './token.interceptor';
import {provideHttpClient, withInterceptors} from "@angular/common/http";

describe('AuthInterceptor', () => {
  beforeEach(() =>
    TestBed.configureTestingModule({
      providers: [provideHttpClient(withInterceptors([tokenInterceptor]))],
    })
  );

  it('should be created', () => {
    const interceptor = TestBed.inject(tokenInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
