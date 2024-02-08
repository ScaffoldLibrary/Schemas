<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://avatars.githubusercontent.com/u/109361408?s=400&u=e1530a760f1b11646a0cd9d13e9776a0c6bdf964&v=4">
    <img src="https://avatars.githubusercontent.com/u/109361408?s=400&u=e1530a760f1b11646a0cd9d13e9776a0c6bdf964&v=4" alt="Logo" width="200" height="200">
  </a>

<h3 align="center">Schemas</h3>

  <p align="center">
    Component-like structures for your ScriptableObjects!
    <br />
    <a href="https://github.com/ScaffoldLibrary/Schemas"><strong>Explore the docs »</strong></a>
    <br />
    <br />
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li>
      <a href="#usage">Usage</a>
      <ul>
        <li><a href="#basics">Basics</a></li>
        <li><a href="#creating-schemas">Creating Schemas</a></li>
        <li><a href="#using-schemas">Using Schemas</a></li>
        <li><a href="#attributes">Attributes</a></li>
        <li><a href="#custom-drawers">Custom Drawers</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://github.com/ScaffoldLibrary/Schemas)

Have you ever found yourself wanting to add Components to ScriptableObjects?<br>
Or maybe hold a polymorphic list, to hold all those cool abstract effects?
<br>
Or at the very least, have different data compositions without relaying on inheritance!

Schemas is here to help! a quick, plug-and-play way to add simple data structures to your objects.

Just inherit from SchemaObject instead of ScriptableObject, and that's it! you got yourself schemas.

```
public class SampleObject : SchemaObject
{

}
```


<p align="right">(<a href="#readme-top">back to top</a>)</p>




<!-- GETTING STARTED -->
## Getting Started

Schemas have no dependencies and no pre-requesities, just install and start to create right away! It's completely self-contained.

### Installation

1. You can also install via git url by adding this entry in your manifest.json
```
"com.scaffold.schemas": "https://https://github.com/ScaffoldLibrary/Schemas.git"
```
<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

### Basics

Schemas is based on 2 main classes, the `SchemaObject` and the `Schema`.

The `SchemaObject` is pretty much a wrapper around ScriptableObject to hold and access whatever Schema you may want. All that is needed is for your `ScriptableObjects` to start Inherting from `SchemaObjects` and it's good to go!


```
public class SampleObject : SchemaObject
{
  //nothing else needed!
}
```

The `Schema` itself is just a base class, just like all your components inherit from `MonoBehaviour`. It has basically no functionality other than providing a reference/base type for the system.

There is no rules for what type of data you will put in your schema, it really is just a data-holder.

The reason we have a empty `Schema` class instead of a Interface is to avoid developers trying to put MonoBehaviours or SO's as schemas, As we are leveraging unity's SerializeReference which does not support reference to those types. 

```
public class SampleSchema : Schema
{
  //any variable can go in here
}
```

### Creating Schemas

To create a `Schema` all you *REALLY* need to do is inherit from `Schema`. It will automatically appear on the dropdown options.

![](https://raw.githubusercontent.com/ScaffoldLibrary/Docs/main/Schemas/Images/screenshot_04.png)


### Using Schemas

Your `SchemaObject` holds the reference to any schema that you may have added to it! usage is very similar to what you would experience with `MonoBehaviours`

*MonoBehaviours*:
```
    public void SomeMethod(GameObject myObj)
    {
        SampleComponent component = myObj.GetComponent<SampleComponent>();
        if(component != null)
        {
            //do something with component
        }
    }
```

*Schemas*:
```
    public void SomeMethod(SchemaObject myObj)
    {
        SampleSchema schema = myObj.GetSchema<SampleSchema>();
        if(schema != null)
        {
            //do something with schema
        }
    }
```

### Attributes

To help customize your schemas, there are 3 utility attributes

`SchemaDescriptionAttribute`: Will provide a tooltip description for your schema

```
[SchemaDescription("Add/Subtract values from player stats while equipped")]
public class Modifiers : CardTrait
{

}
```
![](https://raw.githubusercontent.com/ScaffoldLibrary/Docs/main/Schemas/Images/screenshot_05.png)
<br>
<br>
<br>
`SchemaMenuGroupAttribute`: Will group your schemas on the "Add Schema" dropdown

```
[SchemaMenuGroup("MyGroup")]
public class Breakable : Schema
{

}
```
![](https://raw.githubusercontent.com/ScaffoldLibrary/Docs/main/Schemas/Images/screenshot_06.png)
<br>
<br>
<br>
`SchemaCustomDrawerAttribute`: Mark a class to be used as a custom drawer for a schema

```
[SchemaCustomDrawer(typeof(Modifiers))]
public class ModifierSchemaDrawer : SchemaDrawer
{

}
```

### Custom Drawers

#### For SchemaObjects

SchemaObjects already use a custom drawer by default, when you inherit from `SchemaObjects` it will first draw all the variables of your custom object, and only draw the `Schemas` in the end.

a Overridable version of `SchemaObjectEditor` is being tested and should be released soon.

#### For Schemas

Through the custom inspector of `SchemaObjects`, `Schemas` are not drawn as any Property with a PropertyDrawer, but with a custom Drawer leveraging unity's `GUILayout` and `EditorGUILayout`

to create a custom drawer for a Schema, you can simply create a class inheriting from `SchemaDrawer` and apply the attribute:

```
[SchemaMenuGroup("CardTrait")]
[SchemaDescription("Add/Subtract values from player stats while equipped")]
public class Modifiers : CardTrait
{

    public List<StatModifier> changes = new List<StatModifier>();

    [Serializable]
    public class StatModifier
    {
        public Stats Stat;
        public int Value;
    }
}

[SchemaCustomDrawer(typeof(Modifiers))]
public class ModifierSchemaDrawer : SchemaDrawer
{
    public ModifierSchemaDrawer(SerializedProperty property, SchemaObjectEditor editor) : base(property, editor)
    {

    }

    private SerializedProperty modifiersProp;

    public override void UpdateSerializedProperty(SerializedProperty property)
    {
        base.UpdateSerializedProperty(property);
        modifiersProp = property.FindPropertyRelative("changes");
    }

    public override void DrawBody()
    {
        EditorGUILayout.Space(3);
        for (int i = 0; i < modifiersProp.arraySize; i++)
        {
            var prop = modifiersProp.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(prop.FindPropertyRelative("Stat"), GUIContent.none);
            EditorGUILayout.PropertyField(prop.FindPropertyRelative("Value"), GUIContent.none);
            if (GUILayout.Button("X", GUILayout.Width(30)))
            {
                RemoveModifier(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Add"))
        {
            AddModifier();
        }
        EditorGUILayout.Space(3);
    }

    private void AddModifier()
    {
        var index = modifiersProp.arraySize;
        modifiersProp.InsertArrayElementAtIndex(index);
        modifiersProp.GetArrayElementAtIndex(index).boxedValue = new Modifiers.StatModifier();
        Editor.Refresh();
    }

    private void RemoveModifier(int propIndex)
    {
        modifiersProp.DeleteArrayElementAtIndex(propIndex);
        Editor.Refresh();
    }
}


```

`SchemaDrawer` can override a few options like the Header and Body, to create custom views:


![](https://raw.githubusercontent.com/ScaffoldLibrary/Docs/main/Schemas/Images/screenshot_03.png)



<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ROADMAP -->
## Roadmap

- [ ] Multi-Editing support
- [ ] expose non-generic public methods
- [ ] Support for custom SchemaObject editors
- [ ] Better dropdown for adding schemas
- [ ] Option to filter schema options on dropdown based on type

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Matheus Cohen - matheuscohen@hotmail.com

Project Link: [https://github.com/MgCohen/Schemas](https://github.com/MgCohen/Schemas)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/ScaffoldLibrary/Schemas.svg?style=for-the-badge
[contributors-url]: https://github.com/ScaffoldLibrary/Schemas/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/ScaffoldLibrary/Schemas.svg?style=for-the-badge
[forks-url]: https://github.com/ScaffoldLibrary/Schemas/network/members
[stars-shield]: https://img.shields.io/github/stars/ScaffoldLibrary/Schemas.svg?style=for-the-badge
[stars-url]: https://github.com/ScaffoldLibrary/Schemas/stargazers
[issues-shield]: https://img.shields.io/github/issues/ScaffoldLibrary/Schemas.svg?style=for-the-badge
[issues-url]: https://github.com/ScaffoldLibrary/Schemas/issues
[license-shield]: https://img.shields.io/github/license/ScaffoldLibrary/Schemas.svg?style=for-the-badge
[license-url]: https://github.com/ScaffoldLibrary/Schemas/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/matheus-cohen
[product-screenshot]: https://raw.githubusercontent.com/ScaffoldLibrary/Docs/main/Schemas/Images/gif_01.gif
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 
