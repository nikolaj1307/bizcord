## Week 46 / Saga pattern 

### Task 01

#### Pick a scenario

#### A user gets banned mid-conversation
- Channel: Forcibly remove user from all channels they are in, post a system message that the user was removed
  Compensate: Re-add user to channels, delete system messages

**The hard question:** 
If the ban succeeds but the voice termination fails, is the user banned? What state is the system in?


### Task 02

#### Before You Write Code - Design First
**Step 1: User Management**
- Started by: UserBanned
- Success: UserBanSucceeded
- Failure: UserBanFailed 
- Compensation: UserUnbanned

**Step 2: Channel**
- Started by: UserBanSucceeded
- Success: BanFromChannelsSucceeded
- Failure: BanFromChannelsFailed
- Compensation: ReaddedToChannels

**Step 3: Streaming / VOIP**
- Started by: BanFromChannelsSucceeded
- Success: SessionTerminatedSucceeded
- Failure: SessionTerminatedFailed 
- Compensation: SessionRestored
  
