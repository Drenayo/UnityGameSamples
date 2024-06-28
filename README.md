<div id="top"></div>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- 项目 LOGO -->
<br />
<div align="center">
  <a href="https://github.com/othneildrew/Best-README-Template">
    <img src="other/UnityGameSamplesF.png" alt="Logo" width="500" height="300">
  </a>

  <h3 align="center">UnityGameSamples</h3>

  <p align="center">
    Unity基建开源项目计划
    <br />
    <a href="https://github.com/Drenayo/UnityGameSamples/tree/main/Assets/Demo">查看 Code</a>
    ·
    <a href="https://github.com/Drenayo/UnityGameSamples/issues">反馈 Bug</a>
    ·
    <a href="https://drenayo.notion.site/Unity-Game-Sample-18cc3c76db3946609a18483341b620fe">查看 Demo</a>
    <br />
  </p>
</div>

<!-- 目录 -->
<details>
  <summary>目录</summary>
  <ol>
    <li>
      <a href="#使用流程">使用流程</a>
    </li>
    <li>
      <a href="#如何使用">如何使用</a>
      <ul>
        <li><a href="#1分类">1.分类</a></li>
        <li><a href="#2项目结构">2.项目结构</a></li>
        <li><a href="#3示例说明">3.示例说明</a></li>
        <li><a href="#4快速查找所需示例">4.快速查找所需示</a></li>
        <li><a href="#5Notion使用">5.Notion使用</a></li>
      </ul>
    </li>
    <li><a href="#路线图">路线图</a></li>
    <li><a href="#项目依赖">项目依赖</a></li>
    <li><a href="#如何贡献">如何贡献</a></li>
    <li><a href="#问答部分">问答部分</a></li>
    <li><a href="#联系我们">联系我们</a></li>
    <li><a href="#致谢">致谢</a></li>
  </ol>
</details>



## 使用流程

UnityGameSamples项目旨在提供一系列简单的Unity功能示例，帮助学习者参考和学习某些功能、模块的写法，并迅速熟悉一些知识点的内容，帮助游戏开发者快速找到自己想要的模块，组装自己的游戏，快速验证想法。

![项目使用流程](Other/readme_1.png)
你可以通过上面这张图片了解整个项目的使用流程，有关项目更详细的解读请继续向下阅读。

当学习者遇到特定功能无法实现时,可以查阅本项目的功能目录,找到对应的实现逻辑。每个功能示例都附带视频教程、UML 类图解析、演示案例以及源代码,循序渐进地帮助初学者掌握该功能。

当开发者想要快速寻找自己想要的模块时，查阅本项目的模块目录，找到对应模块，每个模块都附带自述文件，自述文件附带该模块的一切详尽的解释。

---

## 如何使用

如果想要了解该项目，你需要从该项目的主要分类与项目结构入手。

### 1.分类

该项目目前收录4种形式的示例，分别是：

- D : 简单Demo示例
- M : 模块Demo示例
- S : Shader示例
- E : 编辑器工具示例

注意，为了更方便的查找与分类，我为每个分类都设置了一个字母代号，某个分类下收录的示例会分配一个ID编号，ID编号组成结构为：字母代号+3位数数字序号  ----   例如：[D001]

---

**收录示例**：(你可以通过下面的示例看出四种分类分别都用于收录什么内容)

- **简单Demo**：注册登录功能、拼图功能、NavMesh寻路路径绘制、按钮点击进度条功能等...
- **模块Demo**：对话系统、存储系统、武器系统、任务系统等..
- **Shader**：消融效果、水溶效果、红旗飘扬效果等...
- **编辑器工具**：读取Excel工具、自动生成Prefab工具、生成音频工具、Editor动画预览工具等..
---

### 2.项目结构

```
--Art                      所有示例可共享的项目资源,如字体、材质等
--Editor                   编辑器工具示例
    --E001
    --E002
    --ProjectTools         维持项目运转所需工具
--Plugins                  插件目录
--Demo                     简单Demo目录
    --D001-xxx             编号D001-Demo位置                 
    --D002
    --D003
--Modules                  模块示例目录
    --M001
--Shader                   Shader示例目录
    --S001
    --S002
```

---

### 3.示例说明

从上述分类与项目结构可以得知，整个项目的基本概要是由一个个示例组成，示例又分为了几个大类(后续可能会扩充新的分类)，那么每个示例的文件夹中包括示例场景、示例代码以及独立对应资源。

为了让初学者快速了解该示例Demo，我使用Notion构建了一个数据库，每个示例都有一个属于自己的页面，会说明**该示例的ID编号、涉及知识点、代码行数、难度等基本信息**，还会详尽的阐述**如何入手该示例**、**该示例的设计思想**等，同时会附带一个**讲解该示例的视频链接**。

![示例页面](Other/demo_show_D001.png)

---

### 4.快速查找所需示例

如果您想要快速浏览该项目都有哪些示例，除了在线浏览GitHub项目外，还可以通过Notion查找，Notion是一款集成了笔记、知识库、数据表格、看板、日历等多种能力于一体的应用程序。

这里我用到了Notion的数据库功能，方便存储与索引数据，还用到了Notion的看板等功能，方便展示。**遗憾的是Notion不使用科学手段访问较慢**，我在未来计划将这些数据复制一份到国内的FlowUs或wolai(类Notion应用)，但不是现在。

[Notion传送门](https://drenayo.notion.site/UnityGameSample-18cc3c76db3946609a18483341b620fe)| 推荐科学上网

### 5.Notion使用

当你点击链接后，会进入如图所示页面，我将数据分成了两大类，其一，示例库，其二知识库(构建中)。我们可以先点击示例库。
![Notion1](Other/notion_1.png)

示例库如图所示，你可以根据ID，难度，设计知识点等来索引和查询。
![Notion2](Other/notion_2.png)
![Notion3](Other/notion_3.png)

**请看这张示意图！！它可以帮助你快速预览一个Demo是什么效果。当你知道真面目了再点击进去看详细内容，对内容也有了大概了解后，就可以去项目实际体验演示效果，以及看代码。**
![Notion4](Other/notion_4.png)



<!-- 待完成规划 -->
## 路线图

- [ ] 整理知识库、添加知识库链接
- [ ] 为UGS开发预览Wiki编辑器工具
- [ ] 添加网络相关的简易Demo
- [ ] 收录常见Shader
- [ ] 收录算法可视化Demo
- [ ] 编写收录常用编辑器工具案例


到 [OpenIssues](https://github.com/Drenayo/UnityGameSamples/issues) 页查看所有请求的功能 （以及已知的问题）



## 项目依赖

本项目的附加组件和插件列表：

* Unity2021
* 待更新...

## 如何贡献
如果你想要了解某个功能的实现,但该功能并未包含在本项目中,可以提交 Issue 请求(需求写详细一点)。

如果你对某个示例有建议或意见，建议直接在某个Demo的专属Notion页面进行评论。方便我查收，也方便别人查看。

每个页面的右上角，点击三点符号，需要注册账号才能评论。
![Notion评论](Other/notion_p.png)

关于涉及知识点的说明：一个个Demo形成的是知识点网络是本项目的一大亮点，每个Demo都会涉及一些知识点，每个知识点都有一个单独页面，上面罗列了学习该知识点的资料链接，其中有视频、文章、问答贴等等，每个连接还有评语。

为了保证方便查询和索引，所以示例库和知识点库的管理由Notion进行管理。如果你想要短期贡献一段代码，请直接提交PR即可，如果你想要贡献新的知识点或者连接，提交issues即可，我会同步到Notion上，如果你想要长期贡献，直接通过github主页的邮箱与我联系。 


## 问答部分

更新中....


<!-- 联系我们 -->
## 联系我们

[@Drenayo](https://github.com/Drenayo)
邮箱：drenayo@qq.com
B站：[梦小天幼](https://space.bilibili.com/104418839?spm_id_from=333.1007.0.0)

Drenayo 最新更新于 2024.06.28

