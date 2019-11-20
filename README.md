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


## **Wiki**
 [**中文使用文档**][11]
 
## **Reference**

- [帧同步基础库 https://github.com/JiepengTan/LockstepEngine][1]
- [高性能帧同步ECS框架 https://github.com/JiepengTan/LockstepECS][8]
- [代码生成DSL https://github.com/JiepengTan/ME][2]
- [demo: 帧同步版联机版 模拟鲨鱼围捕 2000 条小鱼 https://github.com/JiepengTan/LcokstepECS_Demo_Boid][3]
- [帧同步教程 https://github.com/JiepengTan/Lockstep-Tutorial][4]

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

