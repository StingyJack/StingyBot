// Decompiled with JetBrains decompiler
// Type: Microsoft.Extensions.Configuration.ConfigurationBinder
// Assembly: Microsoft.Extensions.Configuration.Binder, Version=1.1.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: 38F39A52-D963-4A42-B655-90793F4F525F
// Assembly location: C:\Users\Andrew\documents\visual studio 2015\Projects\NodeJsIsCrap\packages\Microsoft.Extensions.Configuration.Binder.1.1.0\lib\netstandard1.1\Microsoft.Extensions.Configuration.Binder.dll

namespace StingyBot.Configuration.MsOverride
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    /// <summary>
  /// Static helper class that allows binding strongly typed objects to configuration values.
  /// </summary>
  public static class ConfigurationBinder
  {
    /// <summary>
    /// Attempts to bind the configuration instance to a new instance of type T.
    /// If this configuration section has a value, that will be used.
    /// Otherwise binding by matching property names against configuration keys recursively.
    /// </summary>
    /// <typeparam name="T">The type of the new instance to bind.</typeparam>
    /// <param name="configuration">The configuration instance to bind.</param>
    /// <returns>The new instance of T if successful, default(T) otherwise.</returns>
    public static T Get<T>(this IConfiguration configuration)
    {
      if (configuration == null)
        throw new ArgumentNullException("configuration");
      object obj = configuration.Get(typeof (T));
      if (obj == null)
        return default (T);
      return (T) obj;
    }

    /// <summary>
    /// Attempts to bind the configuration instance to a new instance of type T.
    /// If this configuration section has a value, that will be used.
    /// Otherwise binding by matching property names against configuration keys recursively.
    /// </summary>
    /// <param name="configuration">The configuration instance to bind.</param>
    /// <param name="type">The type of the new instance to bind.</param>
    /// <returns>The new instance if successful, null otherwise.</returns>
    public static object Get(this IConfiguration configuration, Type type)
    {
      if (configuration == null)
        throw new ArgumentNullException("configuration");
      return ConfigurationBinder.BindInstance(type, (object) null, configuration);
    }

    /// <summary>
    /// Attempts to bind the given object instance to configuration values by matching property names against configuration keys recursively.
    /// </summary>
    /// <param name="configuration">The configuration instance to bind.</param>
    /// <param name="instance">The object to bind.</param>
    public static void Bind(this IConfiguration configuration, object instance)
    {
      if (configuration == null)
        throw new ArgumentNullException("configuration");
      if (instance == null)
        return;
      ConfigurationBinder.BindInstance(instance.GetType(), instance, configuration);
    }

    /// <summary>
    /// Extracts the value with the specified key and converts it to type T.
    /// </summary>
    /// <typeparam name="T">The type to convert the value to.</typeparam>
    /// <param name="configuration">The configuration.</param>
    /// <param name="key">The configuration key for the value to convert.</param>
    /// <returns>The converted value.</returns>
    public static T GetValue<T>(this IConfiguration configuration, string key)
    {
      return configuration.GetValue<T>(key, default (T));
    }

    /// <summary>
    /// Extracts the value with the specified key and converts it to type T.
    /// </summary>
    /// <typeparam name="T">The type to convert the value to.</typeparam>
    /// <param name="configuration">The configuration.</param>
    /// <param name="key">The configuration key for the value to convert.</param>
    /// <param name="defaultValue">The default value to use if no value is found.</param>
    /// <returns>The converted value.</returns>
    public static T GetValue<T>(this IConfiguration configuration, string key, T defaultValue)
    {
      return (T) configuration.GetValue(typeof (T), key, (object) defaultValue);
    }

    /// <summary>
    /// Extracts the value with the specified key and converts it to the specified type.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="type">The type to convert the value to.</param>
    /// <param name="key">The configuration key for the value to convert.</param>
    /// <returns>The converted value.</returns>
    public static object GetValue(this IConfiguration configuration, Type type, string key)
    {
      return configuration.GetValue(type, key, (object) null);
    }

    /// <summary>
    /// Extracts the value with the specified key and converts it to the specified type.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="type">The type to convert the value to.</param>
    /// <param name="key">The configuration key for the value to convert.</param>
    /// <param name="defaultValue">The default value to use if no value is found.</param>
    /// <returns>The converted value.</returns>
    public static object GetValue(this IConfiguration configuration, Type type, string key, object defaultValue)
    {
      string str = configuration.GetSection(key).Value;
      if (str != null)
        return ConfigurationBinder.ConvertValue(type, str);
      return defaultValue;
    }

    private static void BindNonScalar(this IConfiguration configuration, object instance)
    {
      if (instance == null)
        return;
      foreach (PropertyInfo allProperty in ConfigurationBinder.GetAllProperties(instance.GetType().GetTypeInfo()))
        ConfigurationBinder.BindProperty(allProperty, instance, configuration);
    }

    private static void BindProperty(PropertyInfo property, object instance, IConfiguration config)
    {
      if ((object) property.GetMethod == null || !property.GetMethod.IsPublic || property.GetMethod.GetParameters().Length != 0)
        return;
      object instance1 = property.GetValue(instance);
      bool flag = (object) property.SetMethod != null && property.SetMethod.IsPublic;
      if (instance1 == null && !flag)
        return;
      object obj = ConfigurationBinder.BindInstance(property.PropertyType, instance1, (IConfiguration) config.GetSection(property.Name));
      if (!(obj != null & flag))
        return;
      property.SetValue(instance, obj);
    }

    private static object BindToCollection(TypeInfo typeInfo, IConfiguration config)
    {
      Type type = typeof (List<>).MakeGenericType(typeInfo.GenericTypeArguments[0]);
      object instance = Activator.CreateInstance(type);
      Type collectionType = type;
      IConfiguration config1 = config;
      ConfigurationBinder.BindCollection(instance, collectionType, config1);
      return instance;
    }

    private static object AttemptBindToCollectionInterfaces(Type type, IConfiguration config)
    {
      TypeInfo typeInfo = type.GetTypeInfo();
      if (!typeInfo.IsInterface)
        return (object) null;
      if ((object) ConfigurationBinder.FindOpenGenericInterface(typeof (IReadOnlyList<>), type) != null)
        return ConfigurationBinder.BindToCollection(typeInfo, config);
      if ((object) ConfigurationBinder.FindOpenGenericInterface(typeof (IReadOnlyDictionary<,>), type) != null)
      {
        Type type1 = typeof (Dictionary<,>).MakeGenericType(typeInfo.GenericTypeArguments[0], typeInfo.GenericTypeArguments[1]);
        object instance = Activator.CreateInstance(type1);
        Type dictionaryType = type1;
        IConfiguration config1 = config;
        ConfigurationBinder.BindDictionary(instance, dictionaryType, config1);
        return instance;
      }
      Type genericInterface = ConfigurationBinder.FindOpenGenericInterface(typeof (IDictionary<,>), type);
      if ((object) genericInterface != null)
      {
        object instance = Activator.CreateInstance(typeof (Dictionary<,>).MakeGenericType(typeInfo.GenericTypeArguments[0], typeInfo.GenericTypeArguments[1]));
        Type dictionaryType = genericInterface;
        IConfiguration config1 = config;
        ConfigurationBinder.BindDictionary(instance, dictionaryType, config1);
        return instance;
      }
      if ((object) ConfigurationBinder.FindOpenGenericInterface(typeof (IReadOnlyCollection<>), type) != null || (object) ConfigurationBinder.FindOpenGenericInterface(typeof (ICollection<>), type) != null || (object) ConfigurationBinder.FindOpenGenericInterface(typeof (IEnumerable<>), type) != null)
        return ConfigurationBinder.BindToCollection(typeInfo, config);
      return (object) null;
    }

    private static object BindInstance(Type type, object instance, IConfiguration config)
    {
      if ((object) type == (object) typeof (IConfigurationSection))
        return (object) config;
      IConfigurationSection configurationSection = config as IConfigurationSection;
      string str = configurationSection != null ? configurationSection.Value : (string) null;
      if (str != null)
        return ConfigurationBinder.ConvertValue(type, str);
      if (config != null && config.GetChildren().Any<IConfigurationSection>())
      {
        if (instance == null)
        {
          instance = ConfigurationBinder.AttemptBindToCollectionInterfaces(type, config);
          if (instance != null)
            return instance;
          instance = ConfigurationBinder.CreateInstance(type);
        }
        Type genericInterface1 = ConfigurationBinder.FindOpenGenericInterface(typeof (IDictionary<,>), type);
        if ((object) genericInterface1 != null)
          ConfigurationBinder.BindDictionary(instance, genericInterface1, config);
        else if (type.IsArray)
        {
          instance = (object) ConfigurationBinder.BindArray((Array) instance, config);
        }
        else
        {
          Type genericInterface2 = ConfigurationBinder.FindOpenGenericInterface(typeof (ICollection<>), type);
          if ((object) genericInterface2 != null)
            ConfigurationBinder.BindCollection(instance, genericInterface2, config);
          else
            config.BindNonScalar(instance);
        }
      }
      return instance;
    }

    private static object CreateInstance(Type type)
    {
      TypeInfo typeInfo = type.GetTypeInfo();
      if (typeInfo.IsInterface || typeInfo.IsAbstract)
        throw new InvalidOperationException("Microsoft.Extensions.Configuration.Binder.Resources.FormatError_CannotActivateAbstractOrInterface((object) type)");
      if (type.IsArray)
      {
        if (typeInfo.GetArrayRank() > 1)
          throw new InvalidOperationException("Microsoft.Extensions.Configuration.Binder.Resources.FormatError_UnsupportedMultidimensionalArray((object) type)");
        return (object) Array.CreateInstance(typeInfo.GetElementType(), new int[1]);
      }
      IEnumerable<ConstructorInfo> declaredConstructors = typeInfo.DeclaredConstructors;
      Func<ConstructorInfo, bool> func = (Func<ConstructorInfo, bool>) (ctor =>
      {
        if (ctor.IsPublic)
          return ctor.GetParameters().Length == 0;
        return false;
      });
/*
      Func<ConstructorInfo, bool> predicate;
      if (!declaredConstructors.Any<ConstructorInfo>(predicate))
        throw new InvalidOperationException("Microsoft.Extensions.Configuration.Binder.Resources.FormatError_MissingParameterlessConstructor((object) type)");
*/
      try
      {
        return Activator.CreateInstance(type);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException("Microsoft.Extensions.Configuration.Binder.Resources.FormatError_FailedToActivate((object) type)", ex);
      }
    }

    private static void BindDictionary(object dictionary, Type dictionaryType, IConfiguration config)
    {
      TypeInfo typeInfo = dictionaryType.GetTypeInfo();
      Type genericTypeArgument1 = typeInfo.GenericTypeArguments[0];
      Type genericTypeArgument2 = typeInfo.GenericTypeArguments[1];
      if ((object) genericTypeArgument1 != (object) typeof (string))
        return;
      MethodInfo declaredMethod = typeInfo.GetDeclaredMethod("Add");
      foreach (IConfigurationSection child in config.GetChildren())
      {
        object obj = ConfigurationBinder.BindInstance(genericTypeArgument2, (object) null, (IConfiguration) child);
        if (obj != null)
        {
          string key = child.Key;
          declaredMethod.Invoke(dictionary, new object[2]
          {
            (object) key,
            obj
          });
        }
      }
    }

    private static void BindCollection(object collection, Type collectionType, IConfiguration config)
    {
      TypeInfo typeInfo = collectionType.GetTypeInfo();
      Type genericTypeArgument = typeInfo.GenericTypeArguments[0];
      string name = "Add";
      MethodInfo declaredMethod = typeInfo.GetDeclaredMethod(name);
      foreach (IConfigurationSection child in config.GetChildren())
      {
        try
        {
          object obj = ConfigurationBinder.BindInstance(genericTypeArgument, (object) null, (IConfiguration) child);
          if (obj != null)
            declaredMethod.Invoke(collection, new object[1]
            {
              obj
            });
        }
        catch
        {
        }
      }
    }

    private static Array BindArray(Array source, IConfiguration config)
    {
      IConfigurationSection[] array = config.GetChildren().ToArray<IConfigurationSection>();
      int length = source.Length;
      Type elementType = source.GetType().GetElementType();
      Array instance = Array.CreateInstance(elementType, new int[1]
      {
        length + array.Length
      });
      if (length > 0)
        Array.Copy(source, instance, length);
      for (int index = 0; index < array.Length; ++index)
      {
        try
        {
          object obj = ConfigurationBinder.BindInstance(elementType, (object) null, (IConfiguration) array[index]);
          if (obj != null)
            instance.SetValue(obj, new int[1]
            {
              length + index
            });
        }
        catch
        {
        }
      }
      return instance;
    }

    private static object ConvertValue(Type type, string value)
    {
      if ((object) type == (object) typeof (object))
        return (object) value;
      if (type.GetTypeInfo().IsGenericType && (object) type.GetGenericTypeDefinition() == (object) typeof (Nullable<>))
      {
        if (string.IsNullOrEmpty(value))
          return (object) null;
        return ConfigurationBinder.ConvertValue(Nullable.GetUnderlyingType(type), value);
      }
      try
      {
        return TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException("Failed binding", ex);
      }
    }

    private static Type FindOpenGenericInterface(Type expected, Type actual)
    {
      TypeInfo typeInfo = actual.GetTypeInfo();
      if (typeInfo.IsGenericType && (object) actual.GetGenericTypeDefinition() == (object) expected)
        return actual;
      foreach (Type implementedInterface in typeInfo.ImplementedInterfaces)
      {
        if (implementedInterface.GetTypeInfo().IsGenericType && (object) implementedInterface.GetGenericTypeDefinition() == (object) expected)
          return implementedInterface;
      }
      return (Type) null;
    }

    private static IEnumerable<PropertyInfo> GetAllProperties(TypeInfo type)
    {
      List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();
      do
      {
        propertyInfoList.AddRange(type.DeclaredProperties);
        type = type.BaseType.GetTypeInfo();
      }
      while (type != typeof (object).GetTypeInfo());
      return (IEnumerable<PropertyInfo>) propertyInfoList;
    }
  }
}
