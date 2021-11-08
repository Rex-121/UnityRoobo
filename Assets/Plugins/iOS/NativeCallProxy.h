//
//  NativeCallProxy.h
//  UnityHost
//
//  Created by Tyrant on 2021/10/21.
//

//#ifndef NativeCallProxy_h
//#define NativeCallProxy_h
//
//
//#endif /* NativeCallProxy_h */

#import <Foundation/Foundation.h>

@protocol NativeCallsProtocol
@required
- (void) sendMessageToMobileApp:(NSString*)message;
// other methods
@end

__attribute__ ((visibility("default")))
@interface FrameworkLibAPI : NSObject
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>) aApi;

@end
