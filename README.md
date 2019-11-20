# LockstepECS 
## **简介**
LockstepECS  一个基于c# 指针和结构体 的帧同步框架，使用于超大型场景的帧同步游戏
优点：
- 运行速度快，使用指针,和结构体，基本无gc , PureMode 都比Entitas 快两倍，Burst Mode，快四倍以上
- 内存紧凑，预测回滚是否帧状态拷贝快 7000 只鱼的状态拷贝只消耗0.3ms
- API 和 UNITY ECS 非常相似，可以使用同一种编程范式来编写 logic 层 和 view 层
- 无缝兼容UnityECS ，使用条件宏可以切换两种模式，
    - PureMode:纯代码形式，可以直接在服务器中运行逻辑，不依赖Unity 
    - Burst Mode: 模式，直接生成适配Unity ECS Burst+job框架代码的代码，进一步提升运行速度

- github 上的是免费版

## **Reference**

- [帧同步基础库 https://github.com/JiepengTan/LockstepEngine][1]
- [高性能帧同步ECS框架 https://github.com/JiepengTan/LockstepECS][8]
- [代码生成DSL https://github.com/JiepengTan/ME][2]
- [demo: 帧同步版联机版 模拟鲨鱼围捕 2000 条小鱼 https://github.com/JiepengTan/LcokstepECS_Demo_Boid][3]
- [帧同步教程 https://github.com/JiepengTan/Lockstep-Tutorial][4]
- **LockstepECS 建议或bug 群**
928084598，功能仅限提bug和建议

- **Unity DOTS 技术交流 微信群**

<p align="center"> <img src="https://github.com/JiepengTan/JiepengTan.github.io/blob/master/assets/img/blog/MyVX.png?raw=true" width="256"/></p>

```
为了过滤伸手党，不浪费群里人时间，有ECS使用经验后你再加（能独立用ECS写个小游戏）
添加微信请备注 LECS: + 你的github网址
eg:我个人GitHub 地址为 https://github.com/JiepengTan
备注应该填写为：LECS:JiepengTan

```

## **视频链接**
- [环境搭建（Win & Mac）][9]
- [Boid Demo 运行 ][10]

## **TODO**
- 预测回滚 
- 碰撞检测库
- 寻路库
- 序列化库向前兼容


## **Wiki**
 [**中文使用文档**][11]


## **1.安装运行**
### **1.ClientMode**
 - 1.打开场景
![Screen Shot 2019-11-13 at 4.48.20 PM.png](https://upload-images.jianshu.io/upload_images/11593954-fb2066c0652e5cd0.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 2. 确保钩上了 ClientMode  选项
![Screen Shot 2019-11-13 at 4.49.50 PM.png](https://upload-images.jianshu.io/upload_images/11593954-5dafa8d3640934b2.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 3. 运行 游戏
  A  D  控制方向
  Space  释放技能 (Boid demo 才有技能)

### **2.联网模式**
- 1.打开Game.sln 
![Screen Shot 2019-11-13 at 4.59.36 PM.png](https://upload-images.jianshu.io/upload_images/11593954-b4990f7daa51223e.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 2.编译并运行 Game.sln
![[图片上传中...(Screen Shot 2019-11-13 at 4.50.45 PM.png-75d531-1573782602414-0)]
](https://upload-images.jianshu.io/upload_images/11593954-33601348642f30af.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)


- 3.确保关闭了单机模式  ClientMode
![Screen Shot 2019-11-13 at 4.50.45 PM.png](https://upload-images.jianshu.io/upload_images/11593954-0dbb32d16f646c97.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 4.Build 
![Screen Shot 2019-11-13 at 5.03.04 PM.png](https://upload-images.jianshu.io/upload_images/11593954-c97d439635944ffc.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 5.Run Client
![Screen Shot 2019-11-13 at 5.07.33 PM.png](https://upload-images.jianshu.io/upload_images/11593954-7f73394ad639eadf.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 6. 现在是简单帧同步模式 

![Screen Shot 2019-11-13 at 5.09.51 PM.png](https://upload-images.jianshu.io/upload_images/11593954-dbc45db61ba001d9.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)


（Build 版本代码中为了方便测试，做了限制自动绕圈圈，你可以修改他）
![Screen Shot 2019-11-15 at 10.04.02 AM.png](https://upload-images.jianshu.io/upload_images/11593954-30209b83a9618d66.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

## **2. 开发**
### 1. 目录安排
![Screen Shot 2019-11-15 at 10.14.28 AM.png](https://upload-images.jianshu.io/upload_images/11593954-512d44c85f835292.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
- 1. Game.Model
   这里放置的是MVC 中Model and Control 代码（Logic 层）
- 2. Game.View
   这里放置的是MVC 中View 代码（View 层），依赖于Model  层
- 3. Tools.UnsafeECS.ECSDefine.Game 
   这里放置的是你对游戏中 Entity  Component  System 以及全局状态 State 的定义,必须保证该子dll  是能编译的（不能依赖于Model 或 View 层）

### 2. 环境要求
- 1.需要安装NetCore2.2以上
- 2.需要安装Mono
- 3.GitBash


### 3. Tools.UnsafeECS.ECSDefine 定义详解
#### 0. 代码生成

1. Windows 用户
    - 使用gitBash 执行 Client.Unity/DataAndTools/Tools/UpdateAndCodeGen_Win.sh
    
2. Mac 用户
    - 使用命令行执行 Client.Unity/DataAndTools/Tools/UpdateAndCodeGen.sh
    - 或直接双击运行 Client.Unity/DataAndTools/Tools/UpdateAndCodeGen  (注意无后缀)



 [1]: https://github.com/JiepengTan/LockstepEngine
 [2]: https://github.com/JiepengTan/ME
 [3]: https://github.com/JiepengTan/LcokstepECS_Demo_Boid
 [4]: https://github.com/JiepengTan/Lockstep-Tutorial
 [5]: https://github.com/JiepengTan/FishManShaderTutorial
 [6]: https://www.bilibili.com/video/av67829097
 [7]: https://www.bilibili.com/video/av68850334
 [8]: https://github.com/JiepengTan/LockstepECS
 [9]: https://www.bilibili.com/video/av76298196
 [10]: https://www.bilibili.com/video/av76311418
 [11]: https://github.com/JiepengTan/LockstepECS/wiki

